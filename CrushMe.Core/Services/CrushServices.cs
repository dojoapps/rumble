using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrushMe.Core;
using CrushMe.Core.Helpers;
using CrushMe.Core.Models;
using Raven.Client;
using Raven.Client.Extensions;

namespace CrushMe.Core.Services
{
    public class CrushResult
    {
        public bool Success { get; set; }

        public Crush Crush { get; set; }

        public string Message { get; set; }
    }

    public class CrushService
    {
        public const int CrushCandidatesLength = 10;

        private readonly IDocumentSession RavenSession;

        public CrushService(IDocumentSession session)
        {
            this.RavenSession = session;
        }

        public CrushResult Crush(long crusherId, long targetId, string fatherCrushId)
        {
            var now = DateTime.UtcNow;
            var target = RavenSession.Load<User>(targetId);
            var crusher = RavenSession.Load<User>(crusherId);

            if (target == null || crusher == null)
            {
                return new CrushResult()
                {
                    Success = false
                };
            }

            var activeCrushes = RavenSession.Query<Crush>().Where(x => x.CrusherId == RavenSession.BuildRavenId<User>(crusherId) && x.TargetId == RavenSession.BuildRavenId<User>(targetId) && x.Status == CrushStatus.Pending);


            if (activeCrushes.Count() > 3)
            {
                return new CrushResult()
                {
                    Success = false,
                    Message = "Você já possui mais de 3 crushes em aberto para esta pessoa, espere ela responder antes de mandar um novo!"
                };
            }

            var crush = new Crush()
            {
                Candidates = this.CandidatesFor(crusher, target),
                CrusherId = crusher.Id,
                TargetId = target.Id,
                DateCreated = now,
                DateExpires = now.AddDays(30),
                ParentCrushId = fatherCrushId,
                Status = CrushStatus.Pending
                
            };

            RavenSession.Store(crush);

            return new CrushResult() {
                Success = true,
                Crush = crush
            };
        }

        private List<Crush.Candidate> CandidatesFor(User crusher, User target)
        {
            var targetFriendsList = RavenSession.Include<UserFriends>(x => x.FriendsIds).Load(target.Id.ToLongId());

            List<Crush.Candidate> candidates = new List<Crush.Candidate>();
            int friendsToTake = 5;

            if (target.GenderPreference == UserGender.Unknown || crusher.GenderPreference == target.GenderPreference)
            {
                candidates.Add(new Crush.Candidate()
                {
                    Selected = false,
                    UserId = crusher.Id,
                    Name = crusher.Name
                });

                friendsToTake = 4;
            }

            Random rand = new Random();

            if (targetFriendsList != null)
            {
                var friends = RavenSession.Load<User>(targetFriendsList.FriendsIds).ToList();
                if (target.GenderPreference != UserGender.Unknown)
                {
                    friends = friends.Where(x => x.GenderPreference == target.GenderPreference).ToList();
                }
                candidates.AddRange(
                    friends.OrderBy(x => rand.Next(friends.Count())).Take(friendsToTake)
                    .Select(x => new Crush.Candidate() {
                        Selected = false,
                        UserId = x.Id,
                        Name = x.Name
                    })
                );
            }            

            var otherCandidates = RavenSession.Query<User>().Customize(x => x.RandomOrdering()).Take(5);

            if (target.GenderPreference != UserGender.Unknown)
            {
                otherCandidates.Where(x => x.GenderPreference == target.GenderPreference);
            }

            candidates.AddRange(otherCandidates.ToList().Select(x => new Crush.Candidate() { Name = x.Name, UserId = x.Id, Selected = false }));

            

            candidates.Shuffle();

            return candidates;
        }
    }
}