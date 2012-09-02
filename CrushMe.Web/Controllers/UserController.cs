using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrushMe.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [GET("/dashboard")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
