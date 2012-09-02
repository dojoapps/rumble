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
    public class CrushController : BaseController
    {
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
