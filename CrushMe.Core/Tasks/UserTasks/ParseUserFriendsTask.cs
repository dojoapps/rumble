using CrushMe.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrushMe.Core.Tasks.UserTasks
{
    public class ParseUserFriendsTask : BackgroundTask
    {
        public long UserId { get; set; }

        public List<long> FriendsIds { get; set; }

        public ParseUserFriendsTask(long userId, List<long> friends)
        {
            UserId = userId;
            FriendsIds = friends;
        }

        public override void Execute()
        {
            //var existingFriends = DocumentSession.Load<User>(FriendsIds);
        }
    }
}
