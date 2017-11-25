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

            // cadastro de fonte pagadora
            bundles.Add(new Bundle("~/bundles/js/fonte-pagadora")
                        .Include("~/Content/js/cadastros/fonte-pagadora/fonte-pagadora-classes.ko.js")
                        .Include("~/Content/js/cadastros/fonte-pagadora/fonte-pagadora.ko.js")
                        .Include("~/Content/js/cadastros/fonte-pagadora/fonte-pagadora.setup.js"));

            // cadastro de fonte recurso
            bundles.Add(new Bundle("~/bundles/js/fonte-recurso")
                        .Include("~/Content/js/cadastros/fonte-recurso/fonte-recurso-classes.ko.js")
                        .Include("~/Content/js/cadastros/fonte-recurso/fonte-recurso.ko.js")
                        .Include("~/Content/js/cadastros/fonte-recurso/fonte-recurso.setup.js"));

            // cadastro de gerência
            bundles.Add(new Bundle("~/bundles/js/gerencia")
                        .Include("~/Content/js/cadastros/gerencia/gerencia-classes.ko.js")
                        .Include("~/Content/js/cadastros/gerencia/gerencia.ko.js")
                        .Include("~/Content/js/cadastros/gerencia/gerencia-complete.js")
                        .Include("~/Content/js/cadastros/usuario/usuario-complete.js")
                        .Include("~/Content/js/cadastros/gerencia/gerencia.setup.js"));

            // cadastro de usuário
            bundles.Add(new Bundle("~/bundles/js/usuario")
                        .Include("~/Content/js/cadastros/usuario/usuario-classes.ko.js")

                        .Include("~/Content/js/cadastros/cargo/cargo-classes.ko.js")
                        .Include("~/Content/js/cadastros/centro-custo/centro-custo-classes.ko.js")
                        .Include("~/Content/js/cadastros/gerencia/gerencia-classes.ko.js")

                        .Include("~/Content/js/cadastros/usuario/usuario.ko.js")
                        .Include("~/Content/js/cadastros/usuario/usuario.setup.js"));

            // cadastro de gerência
            bundles.Add(new Bundle("~/bundles/js/tipo-despesa")
                        .Include("~/Content/js/cadastros/tipo-despesa/tipo-despesa-classes.ko.js")
                        .Include("~/Content/js/cadastros/tipo-despesa/tipo-despesa.ko.js")
                        .Include("~/Content/js/cadastros/tipo-despesa/tipo-despesa.setup.js"));

            bundles.Add(new Bundle("~/bundles/js/categoria")
                        .Include("~/Content/js/cadastros/categoria/categoria-classes.ko.js")
                        .Include("~/Content/js/cadastros/categoria/categoria.ko.js")
                        .Include("~/Content/js/cadastros/categoria/categoria.setup.js"));
        }
    }
}
