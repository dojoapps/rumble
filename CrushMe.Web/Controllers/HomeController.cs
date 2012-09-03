using AttributeRouting.Web.Mvc;
using CrushMe.Database;
using CrushMe.Database.Models;
using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CrushMe.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public CrushMeContext db = new CrushMeContext();

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

                    var user = db.Users.Find(id);

                    if (user == null)
                    {
                        user = new User()
                        {
                            Id = id,
                            IsActive = true,
                            Name = (string)me.name,
                            Friends = new List<UserFriend>()
                        };

                        db.Users.Add(user);
                    }
                    else if (user.IsActive == false)
                    {
                        user.IsActive = true;
                    }

                    // Atualiza os amigos do usuários
                    dynamic friendsDynamic = client.Get("me/friends");
                    List<dynamic> friends = JsonConvert.DeserializeObject<List<dynamic>>(friendsDynamic.data.ToString());

                    var friendList = friends.Select(x => new User() { Name = x.name, Id = x.id, IsActive = false }).ToList();

                    friendList.ForEach(u =>
                    {
                        var friendUser = db.Users.Find(u.Id);
                        if ( friendUser == null)
                        {
                            friendUser = db.Users.Add(u);
                        }

                        if (!user.Friends.Any(f => f.FbId == u.Id))
                        {
                            user.Friends.Add(new UserFriend()
                            {
                                FbId = u.Id,
                                Name = u.Name
                            });
                        }                        
                    });

                    db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(id.ToString(), false);

                    return RedirectToAction("Index", "CrushList");
                }
                catch (FacebookOAuthException ex)
                {
                    return View("FacebookAuth");
                }
            }
            else if (string.IsNullOrEmpty(error))
            {
                return RedirectToAction("Welcome");
            } 
            else 
            {
                var oauthUrl = string.Format(@"https://www.facebook.com/dialog/oauth/?client_id={0}&redirect_uri={1}",
                    ConfigurationManager.AppSettings["Facebook_AppId"],
                    ConfigurationManager.AppSettings["Facebook_CanvasUrl"]);

               return View("FacebookAuth");
            }
        }

        [GET("/bem-vindo")]
        public ActionResult Welcome()
        {
            var i = db.Users.Count();

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
