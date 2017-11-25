using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Framework.Mvc.Components 
{
    public static class ScriptHelper
    {
        public static HtmlString ScriptSearch(this UrlHelper helper, string action, string controller, string area)
        {
            var urlIndex = helper.Action(action, controller, new { area = area });

            var text = new System.Text.StringBuilder();
            text.AppendLine("var pg = 1;");
            text.AppendLine("var query = $(\"#txtSearch\").val();");
            text.AppendLine("var url = '" + urlIndex + "?pg=' + pg + '&query=' + query;");
            text.AppendLine("window.location.href = url ");
            
            
            return new HtmlString(text.ToString());
        }

        public static string GetBaseUrl()
        {
            var request = System.Web.HttpContext.Current.Request;
            return request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/');
        }
    }
}
