using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrushMe.Database.Infrastructure;
using CrushMe.Web.Models;
using CrushMe.Database.Models;

namespace CrushMe.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/
        [GET("/dashboard")]
        public ActionResult Index()
        {
            return View();
        }

        [GET("/crushes/received")]
        public ActionResult CrushesReceived(int page=0)
        {
            var crushesList = db.Crushes.Where(x => x.TargetId == UserId)
                .OrderByDescending(x => x.DateCreated)
                .Skip(page).Take(10)
                .MapTo<CrushReceivedViewModel>();
           
            return Json(new CrushesReceivedListViewModel() {
                Page = page,
                Crushes = crushesList
            }, JsonRequestBehavior.AllowGet);
        }

        [GET("/crushes/sent")]
        public ActionResult CrushesSent(int page = 0)
        {
            var crushesList = db.Crushes.Where(x => x.CrusherId == UserId)
                .OrderByDescending(x => x.DateCreated)
                .Skip(page).Take(10)
                .MapTo<CrushSentViewModel>();

            return Json(new CrushesSentListViewModel()
            {
                Page = page,
                Crushes = crushesList
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
