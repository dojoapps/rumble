using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FizzWare.NBuilder;
using CrushMe.Core.Models;
using CrushMe.Core.Helpers;
using CrushMe.Core.Services;

namespace Testing.CrushMe.Services.CrushServiceTests
{
    
    public class CrushServiceTests
    {
        public class ValidCrushes : BaseRavenTest
        {
            protected CrushService service;

            public ValidCrushes()
            {
                var existingUsers = Builder<User>.CreateListOfSize(10)
                                        .All().With(x => x.Id, null)
                                        .Build().ToList();

                existingUsers.ForEach(x => {
                    Session.Store(x);
                    var friends = new UserFriends()
                    {
                        Id = Session.BuildRavenId<UserFriends>(x.Id.ToLongId()),
                        FriendsIds = existingUsers.Where(y => y.Id != x.Id).ToList().GetRange(0,5).Select(u => u.Id).ToList()
                    };

                    Session.Store(friends);
                });

                Session.SaveChanges();

                service = new CrushService(Session);
            }

            [Fact]
            public void ItShouldFindCandidatesWithinFriends()
            {
            }
        }
    }
}
