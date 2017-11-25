using System;

namespace HackathonReembolso.Framework.Mvc.Components.Html
{
    public class Html
    {
        public virtual string Id { get; set; }
        public virtual string Class { get; set; }
        public virtual string Role { get; set; }
        public virtual string Title { get; set; }
        public virtual string DataToggle { get; set; }
        public virtual string DataTarget { get; set; }
        public virtual string DataDismiss { get; set; }
        public virtual string DataBind { get; set; }
        public virtual bool Disabled { get; set; }

        public override string ToString()
        {
            var htmlTitle = string.IsNullOrEmpty(Title) ? string.Empty : string.Format("title='{0}'", this.Title);
            var htmlDataToggle = string.IsNullOrEmpty(DataToggle) ? "" : string.Format("data-toggle='{0}'", DataToggle);
            var htmlDataTarget = string.IsNullOrEmpty(DataTarget) ? "" : string.Format("data-toggle='{0}'", DataTarget);
            var htmlDataDismiss = string.IsNullOrEmpty(DataDismiss) ? "" : string.Format("data-dismiss='{0}'", DataDismiss);
            var htmlDataBind = string.IsNullOrEmpty(DataBind) ? "" : string.Format("data-bind='{0}'", DataBind);
            var htmlDataRole = string.IsNullOrEmpty(Role) ? "" : string.Format("role='{0}'", Role);
            var htmlDisabled = !Disabled ? "" : "disabled=''";

            return String.Format("id='{0}' name='{0}' class='{1}' " + htmlTitle + " " + htmlDataRole + " " + htmlDataToggle + " " + htmlDataTarget + " " + htmlDataDismiss + " " + htmlDataBind + " " + htmlDisabled, this.Id, this.Class);
        }
    }
}
