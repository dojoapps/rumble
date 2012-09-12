using AttributeRouting.Web.Mvc;
using CrushMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrushMe.Core.Infrastructure;
using CrushMe.Core.Models;
using CrushMe.Core.Services;
using Raven.Client;
using CrushMe.Core.Helpers;

namespace CrushMe.Web.Controllers
{
    public class CrushController : BaseController
    {
        public CrushController(IDocumentSession session) : base(session)
        {

        }

        [POST("/crush/new")]
        public ActionResult CreateCrush(long targetId)
        {
            var crushService = new CrushServices(RavenSession);

            var crush = crushService.Crush(UserId.ToLongId(), targetId, null);

            return Json(new { success = true });
        }

        [GET("/crush/new")]
        public ActionResult CreateCrush() {
            return View();
        }

        [POST("/crush/pick/{id}")]
        public ActionResult ChoseCandidate(int id, long? candidateFbId) {
            var crush = RavenSession.Load<Crush>(id);
            if (crush == null || !crush.TargetId.Equals(UserId) )
            {
                return HttpNotFound();
            }

            if (candidateFbId.HasValue)
            {
                var candidate = crush.Candidates.FirstOrDefault(x => x.UserId == RavenSession.BuildRavenId<User>(candidateFbId));

                if (candidate != null)
                {
                    candidate.Selected = true;

                    if (candidate.UserId == crush.CrusherId)
                    {
                        crush.Status = CrushStatus.Match;
                    }
                    else
                    {
                       
                        crush.Status = CrushStatus.NoMatch;
                        // var crushService = new CrushServices(db);
                        // crushService.Crush(UserId, candidateFbId.Value, id);
                    }
                }
                else
                {
                    crush.Status = CrushStatus.NoMatch;
                }
            }
            else
            {
                crush.Status = CrushStatus.NoMatch;
            }

            RavenSession.SaveChanges();


            return Json(new { success = true });           
        }

        [GET("/crush/{id}")]
        public ActionResult Details(int id)
        {
            var crush = RavenSession.Load<Crush>(id);
            if (crush == null)
            {
                return HttpNotFound();
            }

            if (crush.TargetId == UserId)
            {
                var viewModel = crush.MapTo<CrushReceivedViewModel>();
                if (crush.Status == CrushStatus.Pending)
                {
                    return PartialView("_CrushReceivedModal", viewModel);
                }
                else
                {
                    return PartialView("_CrushChosenModal", viewModel);
                }
            }
            else if ( crush.CrusherId == UserId )
            {
                var viewModel = crush.MapTo<CrushSentViewModel>();

                return PartialView("_CrushSentModal", viewModel);
            }

            return HttpNotFound("Malandro!");
        }

    }
}
