using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrushMe.Api.FBData;
using Facebook;

namespace CrushMe.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        public String FbAccessToken { get; private set; }

        public BaseApiController()
        {
            if(Request.Headers.Contains(Constants.FbTokenHeader))
            {
                FbAccessToken = Request.Headers.GetValues(Constants.FbTokenHeader).ToString();
                var client = new FacebookClient { AccessToken = FbAccessToken };
                dynamic me = client.Get("me");
            }
        }
    }
}
