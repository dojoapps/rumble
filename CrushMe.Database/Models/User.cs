using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Facebook;

namespace CrushMe.Database.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        [InverseProperty("Crusher")]
        public virtual List<Crush> Crushes { get; set; }

        [InverseProperty("Target")]
        public virtual List<Crush> Targeted { get; set; }

        public virtual List<CrushCandidate> CrushCandidates { get; set; }

        public virtual List<UserFriend> Friends { get; set; } 
    }
}
