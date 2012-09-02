using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrushMe.Database.Models;
using Facebook;
using Newtonsoft.Json;

namespace CrushMe.Database.FBData
{
    public class ApiExplorer
    {
        /// <summary>
        /// Create a user and get this data from FB
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="isActive"> </param>
        /// <returns></returns>
        public static User UserFactory(string accessToken, bool isActive = true)
        {
            var user = new User();
            GetUserBasicData(accessToken, ref user);
            user.IsActive = isActive;
            return user;
        }

        /// <summary>
        /// Update the user data
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="user"></param>
        public static void UserUpdate(string accessToken, ref User user)
        {
            GetUserBasicData(accessToken, ref user);
        }

        /// <summary>
        /// Get user data from FB
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="user"></param>
        private static void GetUserBasicData(string accessToken, ref User user)
        {
            var client = new FacebookClient
            {
                AppSecret = Constants.FbAppSecret,
                AppId = Constants.FbAppId,
                AccessToken = accessToken
            };

            dynamic me = client.Get("me");

            user.Name = (string)me.name;
            user.Id = (long)me.id;
        }

        public static  void UpdateFriends(string accessToken, ref User user)
        {
            var client = new FacebookClient
            {
                AppSecret = Constants.FbAppSecret,
                AppId = Constants.FbAppId,
                AccessToken = accessToken
            };

            var friendsDynamic = client.Get("friends").ToString();
            var friends = JsonConvert.DeserializeObject<List<User>>(friendsDynamic);

            foreach (var friend in friends)
            {
                if(user.Friends.Any(user1 => user1.Name != friend.Name))
                {
                    user.Friends.Add(friend);
                }
            }

        }
    }
}