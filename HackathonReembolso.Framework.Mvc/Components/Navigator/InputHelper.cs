using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Framework.Mvc.Components.Navigator
{
    public static class InputHelper
    {
        public static HtmlString Search(this HtmlHelper helper, string id, string placeHolder)
        {
            var html = new System.Text.StringBuilder();

            html.AppendLine("<div class='input-group'>");
            html.AppendLine("    <input class='form-control' type='text' id='txt" + id + "' placeholder='" + placeHolder + "' />");
            html.AppendLine("    <span class='input-group-btn'>");
            html.AppendLine("        <button class='btn btn-sm btn-default' type='button' id='btn" + id + "'>");
            html.AppendLine("            <i class='icon-search bigger-110'></i>");
            html.AppendLine("        </button>");
            html.AppendLine("    </span>");
            html.AppendLine("</div>");

            return new HtmlString(html.ToString());
        }
    }
}
