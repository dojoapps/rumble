using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Api.Models.Crushes
{
    public class CrushReceivedApiModel
    {
        public long CrusherFbId { get; set; }
        public long TargetFbId { get; set; }
        public DateTime CrushDate { get; set; }
        public int Status { get; set; }
        public List<CrushOptionApiModel> Options { get; set; }
    }
}