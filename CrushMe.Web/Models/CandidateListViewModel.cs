using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Web.Models
{
    public class CandidateListViewModel
    {
        public int Page { get; set; }

        public double PageCount { get; set; }

        public List<CandidateViewModel> Candidates { get; set; }
    }
}