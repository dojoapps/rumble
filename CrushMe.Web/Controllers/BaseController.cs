using CrushMe.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrushMe.Web.Controllers
{
    public class BaseController : Controller
    {
        public CrushMeContext db = new CrushMeContext();
    }
}