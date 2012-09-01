using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CrushMe.Database.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [InverseProperty("Crusher")]
        public List<Crush> Crushes { get; set; }

        [InverseProperty("Target")]
        public List<Crush> Targeted { get; set; }

        public List<CrushOption> CrushOptions { get; set; }
    }
}
