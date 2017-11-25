using System.Web;
using System.Web.Optimization;

namespace HackathonReembolso.Mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new Bundle("~/bundles/js/nav-menu")
                        .Include("~/Content/js/menu/menu-item.js")
                        .Include("~/Content/js/menu/menu.js")
                        .Include("~/Content/js/menu/menu-json-helper.js")
                        .Include("~/Content/js/menu/menu-setup.js"));

            // cadastro de cargos
            bundles.Add(new Bundle("~/bundles/js/cargo")
                        .Include("~/Content/js/cadastros/cargo/cargo-classes.ko.js")
                        .Include("~/Content/js/cadastros/cargo/cargo.ko.js")
                        .Include("~/Content/js/cadastros/cargo/cargo.setup.js"));

            // cadastro de centro de custos
            bundles.Add(new Bundle("~/bundles/js/centro-custo")
                        .Include("~/Content/js/cadastros/centro-custo/centro-custo-classes.ko.js")
                        .Include("~/Content/js/cadastros/centro-custo/centro-custo.ko.js")
                        .Include("~/Content/js/cadastros/centro-custo/centro-custo.setup.js"));
        }
    }
}
