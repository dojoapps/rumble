using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CrushMe.Database.Models
{
    public class CrushCandidate
    {
        public long UserId { get; set; }

        public bool Selected { get; set; }
    }
}
