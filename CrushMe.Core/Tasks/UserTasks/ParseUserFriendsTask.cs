using CrushMe.Core.Models;
using CrushMe.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Facebook;
using Newtonsoft.Json;

namespace CrushMe.Core.Tasks.UserTasks
{
    public class ParseUserFriendsTask : BackgroundTask
    {
        public long UserId { get; set; }

        public string AccessToken { get; set; }

        public ParseUserFriendsTask(long userId, string access_token)
        {
            UserId = userId;
            AccessToken = access_token;
        }

        public override void Execute()
        {
            var facebookClient = new FacebookClient(AccessToken);
            string userTag = DocumentSession.Advanced.DocumentStore.Conventions.FindTypeTagName(typeof(User));

            dynamic friendsData = facebookClient.Get("/fql", new { q = "SELECT uid,name,sex FROM user WHERE uid IN(SELECT uid2 FROM friend WHERE uid1 = me())" });

            List<dynamic> friendsList = JsonConvert.DeserializeObject<List<dynamic>>(friendsData.data.ToString());
            string[] friendsIds = friendsList.Select(x => (string)string.Format("{0}/{1}", userTag, x.uid)).ToArray();
            var friendsDocs = DocumentSession.Load<User>(friendsIds);

            var userFriendsDocId = DocumentSession.BuildRavenId<UserFriends>(UserId);

            var userFriendsDoc = DocumentSession.Load<UserFriends>(userFriendsDocId);

            if (userFriendsDoc == null)
            {
                userFriendsDoc = new UserFriends()
                {
                    Id = DocumentSession.BuildRavenId<UserFriends>(UserId)                    
                };

                DocumentSession.Store(userFriendsDoc);
            }

            userFriendsDoc.FriendsIds = friendsIds.ToList();

            for (var i = 0; i < friendsList.Count; i++)
            {
                if (friendsDocs[i] == null)
                {
                    dynamic friendData = friendsList[i];

                    var userToAdd = new User()
                    {
                        Id = string.Format("{0}/{1}", userTag, friendData.uid),
                        Name = friendData.name,
                        IsActive = false,
                        GenderPreference = UserGender.Unknown,
                        Gender = (friendData.sex == "male") ? UserGender.Male : (friendData.sex == "female") ? UserGender.Female : UserGender.Unknown
                    };

                    DocumentSession.Store(userToAdd);

                    if (i % 500 == 0)
                    {
                        DocumentSession.SaveChanges();
                    }
                }
            }

            DocumentSession.SaveChanges();
        }
    }
}
