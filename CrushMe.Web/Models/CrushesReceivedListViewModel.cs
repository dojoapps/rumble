using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Web.Models
{
    public class CrushesReceivedListViewModel
    {
        public int Page { get; set; }

        public List<CrushReceivedViewModel> Crushes { get; set; }
    }

    public class CrushReceivedViewModel
    {
        public int Id { get; set; }

        public string PrettyDateSent { get; set; }

        public List<CrushCandidateViewModel> Candidates { get; set; }
    }

    public class CrushCandidateViewModel
    {
        public long FbId { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}