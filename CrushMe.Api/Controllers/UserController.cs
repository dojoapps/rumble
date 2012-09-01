using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrushMe.Api.FBData;
using CrushMe.Database.Models;

namespace CrushMe.Api.Controllers
{
    public class UserController : BaseApiController
    {
        public User Put(string fbId)
        {
            if(!db.Users.Any(user => user.FbId == fbId))
            {
                CurrentUser = ApiExplorer.UserFactory(FbAccessToken);
                db.Users.Any(CurrentUser);
            }
            else
            {
                CurrentUser = db.Users.FirstOrDefault(user1 => user1.FbId == fbId);
                ApiExplorer.UserUpdate(FbAccessToken, ref CurrentUser);
            }

            db.SaveChanges();
            return currentUser;
        }
    }
}
