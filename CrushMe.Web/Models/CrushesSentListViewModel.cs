using CrushMe.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Web.Models
{
    public class CrushesSentListViewModel
    {
        public int Page { get; set; }

        public List<CrushSentViewModel> Crushes { get; set; }
    }

    public class CrushSentViewModel
    {
        public int Id { get; set; }

        public long TargetId { get; set; }

        public string TargetName { get; set; }

        public EnumStatusCrush Status { get; set; }

        public string DateSent { get; set; }
    }
}