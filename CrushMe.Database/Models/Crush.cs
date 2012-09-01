using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CrushMe.Database.Models
{
    public class Crush
    {
        public int Id { get; set; }

        public virtual User Crusher { get; set; }
        public long? CrusherId { get; set; }

        public virtual User Target { get; set; }
        public long? TargetId { get; set; }

        public virtual List<CrushOption> Options { get; set; }

        public int FatherCrushId { get; set; }
        public virtual Crush FatherCrush { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateReplied { get; set; }

        public DateTime DateExpires { get; set; }

        public EnumStatusCrush Status { get; set; }
    }

    public enum EnumStatusCrush
    {
        Pending = 0,
        Match = 1,
        NoMatch = 2
    }
}
