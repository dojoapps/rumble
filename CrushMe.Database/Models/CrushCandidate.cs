using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CrushMe.Database.Models
{
    public class CrushCandidate
    {
        public int Id { get; set; }

        [Required]
        public virtual User User { get; set; }
        public int UserId { get; set; }

        [Required]
        public virtual Crush Crush { get; set; }
        public int CrushId { get; set; }

        public bool Selected { get; set; }
    }
}
