using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Framework.Mvc.Components
{
    public static class StatusHelper
    {
        public static HtmlString Status(this HtmlHelper helper, bool status)
        {
            var text = new System.Text.StringBuilder();
            text.AppendLine("<span class='label label-" + (status ? "success" : "important") + "'>" + (status ? "Ativo" : "Inativo") + "</span>");
            return new HtmlString(text.ToString());
        }
        public static HtmlString Status(this HtmlHelper helper, string id, string label, bool status, string data, bool disabled)
        {
            var text = new System.Text.StringBuilder();
            var htmlDisabled = disabled ? "disabled" : "";
            var htmlTitle = status ? "Ativo" : "Inativo";

            text.AppendLine("<label>");
            text.AppendLine("   <input id='" + id + "' data='" + data + "' class='ace ace-switch ace-switch-6' type='checkbox' value='" + status.ToString() + "' checked " + htmlDisabled + "/>");
            text.AppendLine("   <span class='lbl'>" + label + "</span>");
            text.AppendLine("</label>");
            
            return new HtmlString(text.ToString());
        }
    }
}