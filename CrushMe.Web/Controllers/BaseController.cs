using CrushMe.Database;
using CrushMe.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CrushMe.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public CrushMeContext db = new CrushMeContext();
        public User CurrentUser = null;

        public long UserId;

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (Request.IsAuthenticated)
            {
                if (long.TryParse(User.Identity.Name, out UserId))
                {
                    CurrentUser = db.Users.FirstOrDefault(x => x.Id == UserId);

                    if (CurrentUser == null)
                    {
                        FormsAuthentication.SignOut();
                        filterContext.Result = new RedirectResult("/");
                    }
                }
            }
        }
    }
}