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

namespace CrushMe.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IDocumentSession RavenSession { get; set; }

        public HomeController(IDocumentSession session)
        {
            RavenSession = session;
        }

        //
        // GET: /Home/
        [Route("/")]
        public ActionResult Index(string signed_request, string error)
        {
            var client = new FacebookClient();
            client.AppId = ConfigurationManager.AppSettings["Facebook_AppId"];
            client.AppSecret = ConfigurationManager.AppSettings["Facebook_AppSecret"];
            dynamic jsonRequest;
            
            if (!string.IsNullOrEmpty(signed_request) && client.TryParseSignedRequest(signed_request, out jsonRequest))
            {
                if (jsonRequest == null || jsonRequest.oauth_token == null )
                {
                    return View("FacebookAuth");
                }

                client.AccessToken = jsonRequest.oauth_token.ToString();
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
                            Gender = (me.gender == "male") ? UserGender.Male : (me.gender == "female") ? UserGender.Female : UserGender.Unknown
                        };

                        RavenSession.Store(user);
                    }
                    else if (user.IsActive == false)
                    {
                        user.IsActive = true;

                        if ( me.gender ) {
                            user.Gender = (me.gender == "male") ? UserGender.Male : (me.gender == "female") ? UserGender.Female : UserGender.Unknown;
                        }
                    }

                    

                    // Atualiza os amigos do usuários
                    dynamic friendsDynamic = client.Get("me/friends");
                    List<dynamic> friends = JsonConvert.DeserializeObject<List<dynamic>>(friendsDynamic.data.ToString());

                    var userFriends = new UserFriends()
                    {
                        Id = RavenSession.BuildRavenId<UserFriends>(id),
                        FriendsIds = friends.Select(x => RavenSession.BuildRavenId<UserFriends>((long)x.id)).ToList()
                    };

                    RavenSession.Store(userFriends);

                    TaskExecutor.ExcuteLater(new ParseUserFriendsTask(id, friends.Select(x => (long)x.id).ToList()));

                    RavenSession.SaveChanges();

                    FormsAuthentication.SetAuthCookie(user.Id, false);

                    return RedirectToAction("Index", "CrushList");
                }
                catch (FacebookOAuthException ex)
                {
                    return View("FacebookAuth");
                }
            }
            else 
            {
               return View("FacebookAuth");
            }
        }

        [GET("/bem-vindo")]
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
