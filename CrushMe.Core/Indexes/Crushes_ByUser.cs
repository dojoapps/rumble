using CrushMe.Core.Models;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrushMe.Core.Indexes
{
    public class Crushes_ByUser : AbstractIndexCreationTask<Crush>
    {
        public Crushes_ByUser()
        {
            Map = crushes => from c in crushes
                             select new {
                                 c.CrusherId,
                                 c.TargetId,
                                 c.Status
                             };
        }
    }
}
