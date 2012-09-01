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
        public long FbId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        [InverseProperty("Crusher")]
        public List<Crush> Crushes { get; set; }

        [InverseProperty("Target")]
        public List<Crush> Targeted { get; set; }

        public List<CrushOption> CrushOptions { get; set; }

        public List<User> Friends { get; set; } 
    }
}
