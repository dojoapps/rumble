using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrushMe.Core.Models
{
    public class UserFriends
    {
        public string Id { get; set; }

        public List<string> FriendsIds { get; set; }
    }
}
