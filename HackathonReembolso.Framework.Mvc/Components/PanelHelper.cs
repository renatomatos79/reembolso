using System.Web;
using System.Web.Mvc;
using HackathonReembolso.Framework.Mvc.Components.Html;

namespace HackathonReembolso.Framework.Mvc.Components
{
    public static class PanelHelper
    {
        public static HtmlString Panel(this HtmlHelper helper, string id, string header, string body, string footer, string dataBind, PanelHeaderStyle headerStyle = PanelHeaderStyle.Default)
        {
            var panel = new HtmlPanel();
            panel.Id = id;
            panel.Header = header;
            panel.HeaderStyle = headerStyle;
            panel.Footer = footer;
            panel.Body.AppendLine(body);

            return new HtmlString(panel.ToString());
        }        
    }
}