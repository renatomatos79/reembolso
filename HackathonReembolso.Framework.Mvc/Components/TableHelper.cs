using System.Web;
using System.Web.Mvc;
using HackathonReembolso.Framework.Mvc.Components.Html;

namespace HackathonReembolso.Framework.Mvc.Components
{
    public static class TableHelper
    {
        public static HtmlString Table(this HtmlHelper helper, HtmlTable table)
        {
            return new HtmlString(table.ToString());
        }        
    }
}