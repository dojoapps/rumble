using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrushMe.Web.Controllers
{
    public class TestController : Controller
    {
        [GET("/foo")]
        public ActionResult Dump()
        {
            return Content(ConfigurationManager.ConnectionStrings["RavenDB"].ToString());
        }

    }
}
