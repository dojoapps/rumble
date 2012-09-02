using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrushMe.Database;
using CrushMe.Database.Models;

namespace CrushMe.Api.Services
{
    public static class CrushServices
    {
        public const int CrushCandidatesLength = 10;

        public static Crush Crush(CrushMeContext db, long crusherId, long targetId, int? fatherCrushId)
        {
            var now = DateTime.Now;
            var target = db.Users.Find(targetId);
            var crusher = db.Users.Find(crusherId);

            if (target == null || crusher == null)
                throw new ArgumentNullException();

            var candidates = CandidatesFor(db, crusherId, targetId);

            var crush = new Crush()
            {
                Candidates = candidates,
                Crusher = crusher,
                DateCreated = now,
                DateExpires = now.AddDays(30),
                FatherCrush = db.Crushes.Find(fatherCrushId),
                Status = EnumStatusCrush.Pending,
                Target = target
            };

            db.Crushes.Add(crush);
            db.SaveChanges();

            return crush;
        }

        private static List<CrushCandidate> CandidatesFor(CrushMeContext db, long crusherId, long targetId)
        {
            var candidates = (from u in db.Users
                              where u.Id != targetId && u.Id != crusherId
                              orderby Guid.NewGuid()
                              select u)
                              .Take(CrushCandidatesLength - 1).ToList()
                              .Select(x => new CrushCandidate() {
                                Selected = false,
                                User = x
                              }).ToList();
            candidates.Add(new CrushCandidate() { 
                Selected = false,
                User = db.Users.Find(crusherId)
            });

            return candidates;
        }
    }
}