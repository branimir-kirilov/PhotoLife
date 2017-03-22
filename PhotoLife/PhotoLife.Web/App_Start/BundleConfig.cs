using System.Web;
using System.Web.Optimization;

namespace PhotoLife
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/cloudinary").Include(
                "~/Scripts/jquery.ui.widget.js",
                "~/Scripts/jquery.iframe-transport.js",
                "~/Scripts/jquery.fileupload.js",
                "~/Scripts/jquery.cloudinary.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
             "~/Scripts/kendo/kendo.all.min.js",
             "~/Scripts/kendo/kendo.aspnetmvc.min.js",
             "~/Scripts/kendo/kendo.editor.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.less"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
             "~/Content/kendo/kendo.common-bootstrap.min.css",
             "~/Content/kendo/kendo.bootstrap.min.css"));

            bundles.IgnoreList.Clear();
        }
    }
}
