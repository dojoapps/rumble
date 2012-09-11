using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrushMe.Database.Models
{
    public class UserFriend
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public List<long> Friends { get; set; }
    }
}
