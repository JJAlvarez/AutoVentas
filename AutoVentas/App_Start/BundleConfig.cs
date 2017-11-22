using System.Web;
using System.Web.Optimization;

namespace AutoVentas
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/slicknav.css",
                      "~/Content/style.css",
                      "~/Content/responsive.css",
                      "~/Content/animate.css",
                      "~/Content/colors/red.css",
                      "~/Content/colors/jade.css",
                      "~/Content/colors/green.css",
                      "~/Content/colors/blue.css",
                      "~/Content/colors/beige.css",
                      "~/Content/colors/cyan.css",
                      "~/Content/colors/orange.css",
                      "~/Content/colors/peach.css",
                      "~/Content/colors/pink.css",
                      "~/Content/colors/purple.css",
                      "~/Content/colors/sky-blue.css",
                      "~/Content/colors/yellow.css"));

            bundles.Add(new ScriptBundle("~/bundles/scriptsnecesarios").Include(
                      "~/Scripts/jquery-2.1.4.min.js",
                      "~/Scripts/jquery.migrate.js",
                      "~/Scripts/modernizrr.js",
                      "~/Scripts/jquery.fitvids.js",
                      "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/nivo-lightbox.min.js",
                      "~/Scripts/jquery.isotope.min.js",
                      "~/Scripts/jquery.appear.js",
                      "~/Scripts/count-to.js",
                      "~/Scripts/jquery.textillate.js",
                      "~/Scripts/jquery.lettering.js",
                      "~/Scripts/jquery.easypiechart.min.js",
                      "~/Scripts/jquery.nicescroll.min.js",
                      "~/Scripts/jquery.parallax.js",
                      "~/Scripts/mediaelement-and-player.js",
                      "~/Scripts/jquery.slicknav.js"));

            bundles.Add(new ScriptBundle("~/bundles/funciones").Include(
                "~/Scripts/script.js"
                ));
        }
    }
}
