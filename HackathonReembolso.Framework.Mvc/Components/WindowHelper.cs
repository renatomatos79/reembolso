using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using HackathonReembolso.Framework.Mvc.Components.Html;
using InputType = HackathonReembolso.Framework.Mvc.Components.Html.InputType;

namespace HackathonReembolso.Framework.Mvc.Components
{
    public static class WindowHelper
    {
        public static HtmlString ShowConfirmDialog(this HtmlHelper helper, string name, string title, string message)
        {
            var buttons = new List<HtmlButton>
            {
                new HtmlExitButton("btnExitConfirmDialog", "modal", "modal","Não"),
                new HtmlConfirmButton{Text = "Sim"}
            };

            var text = new System.Text.StringBuilder();

            foreach (var b in buttons)
            {
                text.AppendLine(b.ToString());
            }

            return ShowModalDialog(helper, name, title, message, text.ToString());
        }

        public static HtmlString ShowModalDialog(this HtmlHelper helper, string name, string title, string body, string footer)
        {
            var text = new System.Text.StringBuilder();

            text.AppendLine("<div class='modal fade' id='" + name + "' name='" + name + "' role='dialog' aria-hidden='true' aria-labelledby='lblModalTitle'>");
            text.AppendLine("   <div class='modal-dialog'>");
            text.AppendLine("       <div class='modal-content'>");
            text.AppendLine("           <div class='modal-header'>");
            text.AppendLine("               <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>");
            text.AppendLine("               <h4 class='modal-title' id='lblModalTitle'>" + title + "</h4>");
            text.AppendLine("           </div>");
            text.AppendLine("           <div class='modal-body'>");
            text.AppendLine("               " + body);
            text.AppendLine("           </div>");
            text.AppendLine("           <div class='modal-footer'>");
            text.AppendLine("               " + footer);
            text.AppendLine("           </div>");
            text.AppendLine("       </div><!-- /.modal-content -->");
            text.AppendLine("   </div><!-- /.modal-dialog -->");
            text.AppendLine("</div>");

            return new HtmlString(text.ToString());
        }

        public static HtmlString ShowModalCreate(this HtmlHelper helper, string name, string title, string body) 
        {
            var buttons = new List<HtmlButton>
            {
                new HtmlSaveButton() {Id = "btnCreate"}
            };

            var text = new System.Text.StringBuilder();

            foreach (var b in buttons)
            {
                text.AppendLine(b.ToString());
            }

            return ShowModalDialog(helper, name, title, body, text.ToString());
        }

        public static HtmlString ShowModalEdit(this HtmlHelper helper, string name, string title, string body)
        {
            var buttons = new List<HtmlButton>();
            buttons.Add(new HtmlExitButton("btnExitModalEdit", "modal", ""));
            buttons.Add(new HtmlSaveButton() { Id = "btnEdit" });

            var text = new System.Text.StringBuilder();

            foreach (var b in buttons)
            {
                text.AppendLine(b.ToString());
            }

            return ShowModalDialog(helper, name, title, body, text.ToString());
        }
        
        public static HtmlString ShowModalSearch(this HtmlHelper helper, string name)
        {
            var body = new System.Text.StringBuilder();
            //var alert = new HtmlAlert() { Type = AlertType.danger, Id = "divAlertSearch", Message = "Informe o conteúdo da pesquisa", Title = "Atenção" };
            var txtFilter = new HtmlInput() { Id = "txtApplyFilter", Class="form-control", Required = true, Type = InputType.text, PlaceHolder = "texto da pesquisa" };
            //body.AppendLine(alert.ToString());
            body.AppendLine("<div id='divAlertSearch'></div>");
            body.AppendLine(txtFilter.ToString());

            var footer = new System.Text.StringBuilder();
            var btnExit = new HtmlButton() { Id = "btnModalExit", Class = "btn btn-default", DataDismiss = "modal", Text = "Sair" };
            var btnFilter = new HtmlButton() { Id = "btnModalFilter", Class = "btn btn-primary", Text = "Filtrar" };
            footer.AppendLine(btnExit.ToString());
            footer.AppendLine(btnFilter.ToString());

            return ShowModalDialog(helper, name, "Pesquisar", body.ToString(), footer.ToString());
        }

        public static HtmlString ShowModalView(this HtmlHelper helper, string name, string title, string body)
        {
            var buttons = new List<HtmlButton> {new HtmlExitButton()};

            var text = new System.Text.StringBuilder();

            foreach (var b in buttons)
            {
                text.AppendLine(b.ToString());
            }

            return ShowModalDialog(helper, name, title, body, text.ToString());
        }

        public static HtmlString ShowModalLoading(this HtmlHelper helper, string name, string title, string msg)
        {
            return ShowModalDialog(helper, name, title, msg, "");
        }
    }
}