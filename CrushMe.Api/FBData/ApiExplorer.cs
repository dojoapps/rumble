using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrushMe.Database.Models;
using Facebook;

namespace CrushMe.Api.FBData
{
    public class ApiExplorer
    {
        public static User UserFactory(string accessToken)
        {
            var user = new User();
            GetUserData(accessToken, ref user);
            return user;
        }

        public static void UserUpdate(string accessToken, ref User user)
        {
            GetUserData(accessToken, ref user);
        }

        private static void GetUserData(string accessToken ,ref User user)
        {
            var client = new FacebookClient
            {
                AppSecret = Constants.FbAppSecret,
                AppId = Constants.FbAppId,
                AccessToken = accessToken
            };

            dynamic me = client.Get("me");
            
            user.Name = (string) me.name;
            user.FbId = (long) me.id;
        }
    }
}