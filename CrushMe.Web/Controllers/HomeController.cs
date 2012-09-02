﻿using AttributeRouting.Web.Mvc;
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
    public class HomeController : BaseController
    {
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
                            FbId = id,
                            IsActive = true,
                            Name = (string)me.name
                        };

                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    else if (user.IsActive == false)
                    {
                        user.IsActive = true;
                        db.SaveChanges();
                    }

                    FormsAuthentication.SetAuthCookie(id.ToString(), false);

                    return RedirectToAction("Index", "User");
                }
                catch (FacebookOAuthException ex)
                {
                    return RedirectToAction("Welcome");
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

                return Redirect(oauthUrl);
            }
        }

        [GET("/bem-vindo")]
        public ActionResult Welcome()
        {
            return View();
        }

    }
}