using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CrushMe.Database.Models
{
    public class CrushOption
    {
        public int Id { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Crush Crush { get; set; }

        public bool Selected { get; set; }
    }
}
