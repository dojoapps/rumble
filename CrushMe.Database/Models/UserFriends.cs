using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrushMe.Database.Models
{
    public class UserFriend
    {
        public int Id { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }

        public string Name { get; set; }

        public long FbId { get; set; }

    }
}
