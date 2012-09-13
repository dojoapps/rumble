using CrushMe.Core;
using CrushMe.Core.Models;
using Raven.Client;
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
        public IDocumentSession RavenSession { get; set; }

        public User CurrentUser = null;

        public string UserId;

        public BaseController(IDocumentSession session)
        {
            this.RavenSession = session;
        }        

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (Request.IsAuthenticated)
            {
                CurrentUser = RavenSession.Load<User>(User.Identity.Name);

                if (CurrentUser == null)
                {
                    FormsAuthentication.SignOut();
                    filterContext.Result = new RedirectResult("/");
                }
                else
                {
                    if (CurrentUser.Gender == UserGender.Unknown || CurrentUser.GenderPreference == UserGender.Unknown || CurrentUser.GenderPreference == null)
                    {
                        ViewBag.ShowGenderModal = "true";
                    }
                    else
                    {
                        ViewBag.ShowGenderModal = "false";
                    }

                    UserId = CurrentUser.Id;
                }
            }
        }
    }
}