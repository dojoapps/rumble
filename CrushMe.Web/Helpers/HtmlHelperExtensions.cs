using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CrushMe.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DisplayIf(this HtmlHelper helper, bool condition)
        {
            return condition ? null : MvcHtmlString.Create("style=\"display: none\"");
        }
    }
}