using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrushMe.Database.Models;

namespace CrushMe.Api.Models.Crushes
{
    public class CrushSentApiModel
    {
        public int CrushId { get; set; }
        public long TargetFbId { get; set; }
        public DateTime CrushDate { get; set; }
        public EnumStatusCrush Status { get; set; }
    }
}