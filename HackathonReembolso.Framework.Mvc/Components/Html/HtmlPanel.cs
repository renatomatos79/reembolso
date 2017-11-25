using System;
using System.Text;

namespace HackathonReembolso.Framework.Mvc.Components.Html
{
    public enum PanelHeaderStyle { Default, Primary, Success, Info, Warning, Danger }

    public class HtmlPanel : Html
    {
        public virtual PanelHeaderStyle HeaderStyle { get; set; }
        public virtual string Header { get; set; }
        public virtual string Footer { get; set; }

        private System.Text.StringBuilder _Body = new StringBuilder();
        public virtual System.Text.StringBuilder Body
        {
            get
            {
                return _Body;
            }
        }

        public HtmlPanel()
        {
            this.Id = "pnlData";
            this.HeaderStyle = PanelHeaderStyle.Default;
        }

        protected virtual string PanelHeaderStyleToString(PanelHeaderStyle style)
        {
            switch (style)
            {
                case PanelHeaderStyle.Default: 
                    return "panel-default";
                case PanelHeaderStyle.Primary: 
                    return "panel-primary";
                case PanelHeaderStyle.Success: 
                    return "panel-success";
                case PanelHeaderStyle.Info: 
                    return "panel-info";
                case PanelHeaderStyle.Warning: 
                    return "panel-warning";
                default: 
                    return "panel-danger";
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder table = new StringBuilder();
            table.AppendLine(String.Format("<div class='panel {0}' data-bind='{1}'>", this.PanelHeaderStyleToString(this.HeaderStyle), this.DataBind));

            if (!string.IsNullOrEmpty(this.Header))
            {
                table.AppendLine(String.Format("<div class='panel-heading'>{0}</div>", this.Header));
            }

            table.AppendLine("  <div class='panel-body'>");
            table.AppendLine("     " + this.Body.ToString());
            table.AppendLine("  </div>");

            if (!string.IsNullOrEmpty(this.Footer))
            {
                table.AppendLine(String.Format("<div class='panel-footer'>{0}</div>", this.Footer)); 
            }

            table.AppendLine("</div>");

            return table.ToString();
        }
    }
}