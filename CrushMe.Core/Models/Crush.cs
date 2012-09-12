using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CrushMe.Core.Models
{
    public class Crush
    {
        public string Id { get; set; }

        public string CrusherId { get; set; }

        public string TargetId { get; set; }

        public string TargetName { get; set; }

        public List<Crush.Candidate> Candidates { get; set; }

        public string ParentCrushId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateReplied { get; set; }

        public DateTime DateExpires { get; set; }

        public CrushStatus Status { get; set; }

        public class Candidate
        {
            public string UserId { get; set; }

            public string Name { get; set; }

            public bool Selected { get; set; }
        }
    }

    public enum CrushStatus
    {
        Pending = 0,
        Match = 1,
        NoMatch = 2
    }
}
