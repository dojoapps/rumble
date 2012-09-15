using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrushMe.Common.Infrastructure
{
    public interface IFacebookClient
    {
        string AccessToken { get; set; }

        string AppId { get; set; }

        string AppSecret { get; set; }

        object Get(string path);

        object Get(string path, object parameters);

        object Get(object parameters);

        bool TryParseSignedRequest(string signedRequestValue, out object signedRequest);
    }

    public class FacebookClient : Facebook.FacebookClient, IFacebookClient
    {
        public FacebookClient() : base()
        {
            this.AppId = ConfigurationManager.AppSettings["Facebook_AppId"];
            this.AppSecret = ConfigurationManager.AppSettings["Facebook_AppSecret"];
        }

        public FacebookClient(string accessToken)
            : base(accessToken)
        {
            this.AppId = ConfigurationManager.AppSettings["Facebook_AppId"];
            this.AppSecret = ConfigurationManager.AppSettings["Facebook_AppSecret"];
        }
    }
}
