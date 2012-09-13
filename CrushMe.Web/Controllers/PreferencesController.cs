using AttributeRouting.Web.Mvc;
using CrushMe.Web.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrushMe.Web.Controllers
{
    public class PreferencesController : BaseController
    {
        public PreferencesController(IDocumentSession session) : base(session)
        {

        }
        [GET("/preferences")]
        public ActionResult Index()
        {
            var preferencesModel = new PreferencesViewModel()
            {
                GenderPreference = CurrentUser.GenderPreference
            };

            return PartialView("_GenderModal", preferencesModel);
        }

        [POST("/preferences")]
        public ActionResult Save(PreferencesViewModel inputModel)
        {
            CurrentUser.GenderPreference = inputModel.GenderPreference;

            RavenSession.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}
