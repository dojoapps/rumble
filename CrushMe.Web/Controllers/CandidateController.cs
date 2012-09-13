using AttributeRouting.Web.Mvc;
using CrushMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrushMe.Core.Infrastructure;
using Raven.Client;
using Raven.Client.Linq;
using CrushMe.Core.Models;
using CrushMe.Core.Helpers;
using CrushMe.Core.Indexes;

namespace CrushMe.Web.Controllers
{
    public class CandidateController : BaseController
    {
        public CandidateController(IDocumentSession session) : base(session)
        {

        }

        [GET("/candidates")]
        public ActionResult List(string query, int page=0)
        {
            CandidateListViewModel viewModel = new CandidateListViewModel();

            var userFriendsList = RavenSession.Include<UserFriends>(x => x.FriendsIds).Load(CurrentUser.Id.ToLongId());

            if (string.IsNullOrEmpty(query) && userFriendsList != null)
            {
                var userFriends = RavenSession.Load<User>(userFriendsList.FriendsIds).Where(x => x != null).ToList();

                viewModel.Candidates = userFriends.OrderBy(x => x.Name).MapTo<CandidateViewModel>();
                viewModel.PageCount = Math.Ceiling(userFriends.Count / 50d);
            }
            else
            {
                RavenQueryStatistics stats;

                var candidatesQuery = RavenSession.Query<User, Users_Index>().Statistics(out stats).Where(x => x.Name == query).OrderBy(x => x.Name).ToList();

                viewModel.Page = page;
                viewModel.PageCount = Math.Ceiling(stats.TotalResults / 50d);
                viewModel.Candidates = candidatesQuery.MapTo<CandidateViewModel>();

            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
