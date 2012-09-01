using System;
using System.Collections.Generic;
using System.Linq;
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
        public FacebookClient FbClient { get; private set; }

        public BaseApiController()
        {
            FbClient = new FacebookClient();
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            if (Request.Headers.Contains(Constants.FbTokenHeader))
            {
                FbClient.AccessToken = Request.Headers.GetValues(Constants.FbTokenHeader).First().ToString();
                dynamic me = FbClient.Get("me");

                long id = long.Parse(me.id);
                CurrentUser = db.Users.Find(id);
            }
        }
    }
}
