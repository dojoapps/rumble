using System.Web;
using System.Web.Mvc;
using CrushMe.Api.App_Start;

namespace CrushMe.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new FacebookFilter());
        }

    }
}