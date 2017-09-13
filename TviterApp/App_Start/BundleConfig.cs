using System.Web;
using System.Web.Optimization;

namespace TviterApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

       
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                       "~/Scripts/angular.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularRoute").Include(
                       "~/Scripts/angular-route.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularAnimate").Include(
                       "~/Scripts/angular-animate.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularTouch").Include(
                       "~/Scripts/angular-touch.js"));
            bundles.Add(new ScriptBundle("~/bundles/uibootstrap").Include(
                      "~/Scripts/ui-bootstrap-tpls-2.5.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularCustom").IncludeDirectory(
                     "~/Angular/App", "*.js", true
                 ));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-cerulean.css",
                      "~/Content/site.css"));
        }
    }
}
