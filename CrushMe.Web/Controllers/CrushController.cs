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

        [POST("/crush/{id}")]
        public ActionResult ChoseCandidate(int id, long candidateFbId) {
            var crush = db.Crushes.FirstOrDefault(x => x.Id == id);
            if (crush == null || crush.TargetId != UserId)
            {
                return HttpNotFound();
            }

            var candidate = crush.Candidates.FirstOrDefault(x => x.UserId == candidateFbId);

            if (candidate != null)
            {
                candidate.Selected = true;
                db.SaveChanges();

                return Json(new { success = true });
            }


            return Json(new { success = false });            
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

                return View("_CrushReceivedModal", viewModel);
            }
            else if ( crush.CrusherId == UserId )
            {
                var viewModel = crush.MapTo<CrushSentViewModel>();

                return View("_CrushSentModal", viewModel);
            }

            return HttpNotFound("Malandro!");
        }

    }
}
