using AttributeRouting.Web.Mvc;
using CrushMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrushMe.Database.Infrastructure;
using CrushMe.Database.Models;
using CrushMe.Database.Services;

namespace CrushMe.Web.Controllers
{
    public class CrushController : BaseController
    {
        [POST("/crush/new")]
        public ActionResult CreateCrush(long targetId)
        {
            var crushService = new CrushServices(db);

            var crush = crushService.Crush(UserId, targetId, null);

            return Json(new { success = true });
        }

        [GET("/crush/new")]
        public ActionResult CreateCrush() {
            return View();
        }

        [POST("/crush/chose/{id}")]
        public ActionResult ChoseCandidate(int id, long? candidateFbId) {
            var crush = db.Crushes.FirstOrDefault(x => x.Id == id);
            if (crush == null || crush.TargetId != UserId)
            {
                return HttpNotFound();
            }

            if (candidateFbId.HasValue)
            {
                var candidate = crush.Candidates.FirstOrDefault(x => x.UserId == candidateFbId);

                if (candidate != null)
                {
                    candidate.Selected = true;

                    if (candidate.UserId == crush.CrusherId)
                    {
                        crush.Status = EnumStatusCrush.Match;
                    }
                    else
                    {
                        var crushService = new CrushServices(db);

                        crushService.Crush(UserId, candidateFbId.Value, id);
                    }
                }
                else
                {
                    crush.Status = EnumStatusCrush.NoMatch;
                }
            }
            else
            {
                crush.Status = EnumStatusCrush.NoMatch;
            }

            db.SaveChanges();


            return Json(new { success = true });           
        }

        [GET("/crush/{id}")]
        public ActionResult Details(int id)
        {
            var crush = db.Crushes.FirstOrDefault(x => x.Id == id);
            if (crush == null)
            {
                return HttpNotFound();
            }

            if (crush.TargetId == UserId)
            {
                var viewModel = crush.MapTo<CrushReceivedViewModel>();

                return PartialView("_CrushReceivedModal", viewModel);
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
