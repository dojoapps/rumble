using System.Web;
using System.Web.Optimization;

namespace CrushMe.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-1.*").Include("~/Scripts/bootstrap*").IncludeDirectory("~/Scripts/plugins","*.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //// use this BundleConfig OR add the 3 lines below to your existing BundleConfig.cs and delete this file.
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var css = new StyleBundle("~/Content/css").Include("~/Content/site.less");
            css.Transforms.Add(new LessMinify());
            bundles.Add(css);  
        }
    }
}