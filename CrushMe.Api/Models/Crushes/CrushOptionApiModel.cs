using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrushMe.Api.Models.Crushes
{
    public class CrushOptionApiModel
    {
        public long FbId { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
