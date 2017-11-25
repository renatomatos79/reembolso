namespace HackathonReembolso.Framework.Mvc.Components.Html
{
    public enum AlertType { success, danger, warning, info }
    public class HtmlAlert : Html
    {
        public AlertType Type { get; set; }
        public string Message { get; set; }

        public virtual string AlertTypeToHtmlClass(AlertType type)
        {
            if (type == AlertType.success)
            {
                return "alert-success";
            }
            else if (type == AlertType.danger)
            {
                return "alert-danger";
            }
            else if (type == AlertType.warning)
            {
                return "alert-warning";
            }
            else
            {
                return "alert-info";
            }
        }

        public override string ToString()
        {
            var css = AlertTypeToHtmlClass(this.Type);
            var title = string.IsNullOrEmpty(this.Title) ? string.Empty : string.Format("<strong style='margin-right: 5px'>{0}</strong>", this.Title);
            
            var html = new System.Text.StringBuilder();

            html.AppendLine("<div id='" + this.Id + "' class='alert " + css + " alert-dismissable'>");
            html.AppendLine("    <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>");
            html.AppendLine(     title + this.Message);
            html.AppendLine("</div>");

            return html.ToString();
        }
    }
}
