using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrushMe.Core.Infrastructure;
using CrushMe.Web.Models;
using CrushMe.Core.Models;
using Raven.Client;

namespace CrushMe.Web.Controllers
{
    public class CrushListController : BaseController
    {
        public CrushListController(IDocumentSession session) : base(session)
        {

        }
        
        [GET("/dashboard")]
        public ActionResult Index()
        {
            return View();
        }

        [GET("/crushes/received")]
        public ActionResult CrushesReceived(int page=0)
        {
            var crushesList = RavenSession.Query<Crush>().Where(x => x.TargetId == UserId)
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
            var crushesList = RavenSession.Query<Crush>().Where(x => x.CrusherId == UserId && x.ParentCrushId == null)
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
