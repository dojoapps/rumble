using AttributeRouting.Web.Mvc;
using CrushMe.Core;
using CrushMe.Core.Models;
using Facebook;
using Newtonsoft.Json;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CrushMe.Web.Helpers;
using CrushMe.Core.Tasks;
using CrushMe.Core.Tasks.UserTasks;
using CrushMe.Core.Helpers;
using CrushMe.Common.Infrastructure;

namespace CrushMe.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IDocumentSession RavenSession { get; set; }

        private IFacebookClient client;

        private IFormsAuthentication formsAuth;

        public HomeController(IDocumentSession session, IFacebookClient client)
        {
            RavenSession = session;
            this.client = client;
        }
        [GET("/login")]
        public ActionResult Login(string accessToken)
        {
            client.AppId = ConfigurationManager.AppSettings["Facebook_AppId"];
            client.AppSecret = ConfigurationManager.AppSettings["Facebook_AppSecret"];

            client.AccessToken = accessToken;
            try
            {
                dynamic me = client.Get("me");

                long id = 0;
                long.TryParse(me.id, out id);

                var user = RavenSession.Load<User>(id);

                if (user == null)
                {
                    user = new User()
                    {
                        Id = RavenSession.BuildRavenId<User>(id),
                        IsActive = true,
                        Name = (string)me.name,
                        GenderPreference = UserGender.Unknown,
                        Gender = (me.gender == "male") ? UserGender.Male : (me.gender == "female") ? UserGender.Female : UserGender.Unknown
                    };

                    RavenSession.Store(user);
                }
                else if (user.IsActive == false)
                {
                    user.IsActive = true;

                    if (!string.IsNullOrEmpty(me.gender))
                    {
                        user.Gender = (me.gender == "male") ? UserGender.Male : (me.gender == "female") ? UserGender.Female : UserGender.Unknown;
                    }
                }

                TaskExecutor.ExcuteLater(new ParseUserFriendsTask(id, client.AccessToken));

                RavenSession.SaveChanges();

                FormsAuthentication.SetAuthCookie(user.Id, false);

                return RedirectToAction("Index", "CrushList");
            }
            catch (FacebookOAuthException ex)
            {
                return View("FacebookAuth");
            }
        }

        [Route("/canvas")]
        public ActionResult Canvas(string signed_request, string error)
        {
            dynamic jsonRequest;
            
            if (!string.IsNullOrEmpty(signed_request) && client.TryParseSignedRequest(signed_request, out jsonRequest))
            {
                if (jsonRequest == null || jsonRequest.oauth_token == null )
                {
                    return View("FacebookAuth");
                }

                return Login(jsonRequest.oauth_token.ToString());
                
            }
            else if (error == "access_denied")
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                return View("FacebookAuth");
            }
        }

        [GET("/")]
        public ActionResult Welcome()
        {
            return View();
        }

        [GET("/como-funciona")]
        public ActionResult HowItWorks()
        {
            return View();
        }

        [GET("/festa-de-lancamento")]
        public ActionResult CrushMeParty()
        {
            return View();
        }

        [GET("/sobre")]
        public ActionResult About()
        {
            return View();
        }
    }
}
