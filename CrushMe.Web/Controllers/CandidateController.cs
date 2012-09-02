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
                viewModel.Candidates = CurrentUser.Friends.MapTo<CandidateViewModel>();
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
