using System.Web;
using System.Web.Mvc;
using HackathonReembolso.Framework.Mvc.Components.Html;

namespace HackathonReembolso.Framework.Mvc.Components
{
    public static class ButtonHelper
    {
        public static HtmlString Button(this HtmlHelper helper, string value, string classCss, string path)
        {
            var text = new System.Text.StringBuilder();

            text.Append("<input type='button' ");
            text.Append("value='" + value + "' ");
            text.Append("class='" + classCss + "' ");
            text.Append("onclick='javascript:window.location.href ='" + path + "'' ");
            text.Append("/>");

            return new HtmlString(text.ToString());
        }

        public static HtmlString NewButton(this HtmlHelper helper, string id)
        {
            var btn = new HtmlNewButton();

            if (!string.IsNullOrEmpty(id))
            {
                btn.Id = id;
            }

            return new HtmlString(btn.ToString());
        }
        
        public static HtmlString TableButtonEdit(this HtmlHelper helper, string url)
        {
            var btn = new TableButton() { Id = "btnEdit", Href = url, Title = "Editar", Icon = "icon-pencil" };
            return new HtmlString(btn.ToString());
        }

        public static HtmlString TableButtonDelete(this HtmlHelper helper, string url)
        {
            var btn = new TableButton() { Id = "btnDelete", Href = url, Title = "Excluir", Icon = "icon-trash" };
            return new HtmlString(btn.ToString());
        }
    }
}