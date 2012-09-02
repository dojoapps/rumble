using AttributeRouting.Web.Mvc;
using CrushMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrushMe.Database.Infrastructure;

namespace CrushMe.Web.Controllers
{
    public class CandidateController : BaseController
    {
        //
        // GET: /Candidate/
        [GET("/candidates")]
        public ActionResult List(string query, int page=0)
        {
            CandidateListViewModel viewModel = new CandidateListViewModel();

            if (string.IsNullOrEmpty(query) && CurrentUser.Friends != null)
            {
                viewModel.Candidates = CurrentUser.Friends.OrderBy(x => x.Name).MapTo<CandidateViewModel>();
                viewModel.PageCount = Math.Ceiling(CurrentUser.Friends.Count / 50d);
            }
            else
            {
                
                viewModel.Candidates = CurrentUser.Friends.Where(x => x.Name.Contains(query)).MapTo<CandidateViewModel>();
                var userCandidates = db.Users.Where(x => x.Name.Contains(query)).OrderBy(x => x.Name).MapTo<CandidateViewModel>();

                viewModel.Candidates.AddRange(userCandidates);

                viewModel.Candidates = viewModel.Candidates.GroupBy(x => x.FbId).Select(x => x.First()).OrderBy(x => x.Name).ToList();

            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
