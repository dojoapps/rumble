﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrushMe.Api.FBData;
using CrushMe.Database;
using CrushMe.Database.Models;
using Facebook;

namespace CrushMe.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        public CrushMeContext db = new CrushMeContext();
        public string FbAccessToken { get; private set; }
        public User CurrentUser { get; set; }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        
            if(Request.Headers.Contains(Constants.FbTokenHeader))
            {
                FbAccessToken = Request.Headers.GetValues(Constants.FbTokenHeader).ToString();
                var client = new FacebookClient { AccessToken = FbAccessToken };
                dynamic me = client.Get("me");

                int id = me.id;
                if(db.Users.Any(user => user.FbId == id))
                    CurrentUser = db.Users.Find(id);
            }
        }
    }
}
