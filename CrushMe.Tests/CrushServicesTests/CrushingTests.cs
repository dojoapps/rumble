using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrushMe.Api.Services;
using CrushMe.Database;
using Xunit;

namespace CrushMe.Tests.CrushServicesTests
{
    public class CrushingTests
    {
        public CrushMeContext db { get; set; }
        public long CurrentUserId { get; set; }
        public long TargetId { get; set; }

        public CrushingTests()
        {
            db = new CrushMeContext();
            TargetId = db.Users.First().FbId;
            CurrentUserId = db.Users.OrderBy(x => x.FbId).Skip(1).First().FbId;
        }

        [Fact]
        public void CrushTest()
        {
            var ret = CrushServices.Crush(db, CurrentUserId, TargetId, null);
            Assert.NotNull(ret.Target);
            Assert.NotNull(ret.Crusher);
            Assert.Equal(CrushServices.CrushCandidatesLength, ret.Candidates.Count());
            Assert.Contains(ret.Crusher, ret.Candidates.Select(x => x.User));
            Assert.DoesNotContain(ret.Target, ret.Candidates.Select(x => x.User));
            //TODO MORE ASSERTS
        }
    }
}
