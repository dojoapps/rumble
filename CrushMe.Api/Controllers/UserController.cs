using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrushMe.Database.FBData;
using CrushMe.Database.Models;

namespace CrushMe.Api.Controllers
{
    public class UserController : BaseApiController
    {
        public User Put(long fbId)
        {
            User newUser;
            if(!db.Users.Any(user => user.Id == fbId))
            {
                newUser = ApiExplorer.UserFactory(FbAccessToken, true);
                db.Users.Add(CurrentUser);
            }
            else
            {
                newUser = db.Users.FirstOrDefault(user1 => user1.Id == fbId);
                ApiExplorer.UserUpdate(FbAccessToken, ref newUser);

                if (!newUser.IsActive) newUser.IsActive = true;
            }

            CurrentUser = newUser;

            db.SaveChanges();
            return CurrentUser;
        }

        public List<User> Friends()
        {
            var user = CurrentUser;
            ApiExplorer.UpdateFriends(FbAccessToken, ref user);

            return user.Friends;
        }
    }
}
