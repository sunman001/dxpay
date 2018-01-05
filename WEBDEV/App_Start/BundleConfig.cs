using System.Web.Optimization;

namespace WEBDEV
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/help/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/help/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/site.css"));
            BundleTable.EnableOptimizations = false;
        }
    }
}