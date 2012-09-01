using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrushMe.Api.ApiModel;
using CrushMe.Api.FBData;
using CrushMe.Database;
using Facebook;

namespace CrushMe.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        CrushMeContext db = new CrushMeContext();
        public string FbAccessToken { get; private set; }
        public User CurrentUser { get; set; }

        public BaseApiController()
        {
            if(Request.Headers.Contains(Constants.FbTokenHeader))
            {
                FbAccessToken = Request.Headers.GetValues(Constants.FbTokenHeader).ToString();
                var client = new FacebookClient { AccessToken = FbAccessToken };
                dynamic me = client.Get("me");

                CurrentUser = db.Users.Find(me.id);
            }
        }
    }
}
