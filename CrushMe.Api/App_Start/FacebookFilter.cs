using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CrushMe.Api.FBData;
using Facebook;

namespace CrushMe.Api.App_Start
{
    public class FacebookFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                if (!AuthorizeRequest(actionContext.ControllerContext.Request))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        RequestMessage = actionContext.ControllerContext.Request
                    };
                }
                return;
            }
        }

        private bool AuthorizeRequest(HttpRequestMessage request)
        {
            bool authorized = false;
            if (request.Headers.Contains(Constants.FbTokenHeader))
            {
                var tokenValue = request.Headers.GetValues(Constants.FbTokenHeader);
                if (tokenValue.Count() == 1)
                {
                    var aToken = tokenValue.FirstOrDefault();

                    var client = new FacebookClient { AccessToken = aToken };
                    dynamic me = client.Get("me");
                    
                    //TODO: if db contains me.id return true
                    //TODO: else return false

                    authorized = true;
                }
            }
            return authorized;
        }
    }
}