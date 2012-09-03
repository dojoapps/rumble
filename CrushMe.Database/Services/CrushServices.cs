using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrushMe.Database;
using CrushMe.Database.Models;

namespace CrushMe.Database.Services
{
    public class CrushResult
    {
        public bool Success { get; set; }

        public Crush Crush { get; set; }

        public string Message { get; set; }
    }

    public class CrushServices
    {
        public const int CrushCandidatesLength = 10;

        private readonly CrushMeContext db;

        public CrushServices(CrushMeContext db)
        {
            this.db = db;
        }

        public CrushResult Crush(long crusherId, long targetId, int? fatherCrushId)
        {
            var now = DateTime.UtcNow;
            var target = db.Users.Find(targetId);
            var crusher = db.Users.Find(crusherId);

            if (target == null || crusher == null)
                return new CrushResult()
                {
                    Success = false
                };

            if (db.Crushes.Count(x => x.CrusherId == crusherId && x.TargetId == targetId && x.Status == EnumStatusCrush.Pending) > 3)
            {
                return new CrushResult()
                {
                    Success = false,
                    Message = "Você já possui mais de 3 crushes em aberto para esta pessoa, espere ela responder antes de mandar um novo!"
                };
            }

            var candidates = this.CandidatesFor(crusherId, targetId);

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

            return new CrushResult() {
                Success = true,
                Crush = crush
            };
        }

        private List<CrushCandidate> CandidatesFor(long crusherId, long targetId)
        {
            var target = db.Users.Find(targetId);
            List<long> preCandidateIds;
            if (target != null)
            {
                preCandidateIds = target.Friends.Select(x => x.FbId).ToList();
            }
            else
            {
                preCandidateIds = db.Users.Select(x => x.Id).ToList();
            }

            var candidates = (from pcId in preCandidateIds
                              where pcId != targetId && pcId != crusherId
                              orderby Guid.NewGuid()
                              select pcId)
                              .Take(CrushCandidatesLength - 1).ToList()
                              .Select(x => new CrushCandidate() {
                                Selected = false,
                                UserId = x
                              }).ToList();

            candidates.Add(new CrushCandidate() {
                Selected = false,
                User = db.Users.Find(crusherId)
            });

            return candidates;
        }
    }
}