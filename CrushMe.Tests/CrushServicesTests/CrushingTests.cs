using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrushMe.Database.Services;
using CrushMe.Database;
using CrushMe.Database.Models;
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
            TargetId = db.Users.First().Id;
            CurrentUserId = db.Users.OrderBy(x => x.Id).Skip(1).First().Id;
        }

        [Fact]
        public void CrushTest()
        {
            var crushService = new CrushServices(db);

            var result = crushService.Crush(CurrentUserId, TargetId, null);

            var ret = result.Crush;
            Assert.NotNull(ret.Target);
            Assert.Equal(TargetId, ret.Target.Id);
            Assert.NotNull(ret.Crusher);
            Assert.Equal(CurrentUserId, ret.Crusher.Id);
            Assert.Equal(CrushServices.CrushCandidatesLength, ret.Candidates.Count());
            Assert.Contains(ret.Crusher, ret.Candidates.Select(x => x.User));
            Assert.DoesNotContain(ret.Target, ret.Candidates.Select(x => x.User));
            Assert.Equal(EnumStatusCrush.Pending, ret.Status);
        }
    }
}
