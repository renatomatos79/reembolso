using System;

namespace HackathonReembolso.Framework.Mvc.Components.Html
{
    public enum ButtonType { submit, button, resert }

    public enum ButtonHorizontalAlign { none, left, right }
    public enum ButtonSize { default_button, large, small, extra_small }

    public enum ButtonStyle { default_button, primary, success, info, warning, danger, link }
    public class HtmlButton : Html
    {
        public virtual string Icon { get; set; }
        public virtual string Text { get; set; }
        public virtual ButtonType Type { get; set; }

        public virtual ButtonHorizontalAlign HorizontalAlign { get; set; }
        public virtual ButtonSize ButtonSize { get; set; }
        public virtual ButtonStyle ButtonStyle { get; set; }

        public HtmlButton()
        {
            this.Type = ButtonType.button;
        }

        protected virtual string ButtonTypeToString(ButtonType type)
        {
            if (type == ButtonType.submit)
            {
                return "submit";
            }
            else if (type == ButtonType.resert)
            {
                return "resert";
            }
            else
            {
                return "button";
            }
        }

        public override string ToString()
        {
            var htmlText = base.ToString();
            return String.Format("<button type='{0}' " + htmlText + "><i class='{1}'></i>{2}</button>", ButtonTypeToString(this.Type), this.Icon, this.Text);
        }
    }

    public class TableButton : HtmlButton
    {
        public virtual string Href { get; set; }
        public override string ToString()
        {
            return String.Format("<a id='{0}' href='{1}' rel='tooltip' title='{2}'><i class='{3}'></i></a>", this.Id, this.Href, this.Title, this.Icon);
        }
    }

    public class HtmlNewButton : HtmlButton
    {
        public HtmlNewButton()
        { 
            this.Id = "btnNew";
            this.Class = "btn btn-primary";
            this.DataToggle = "modal";
            this.DataTarget = "#divCreate";
            this.Icon = "icon-plus";
            this.Text = "Novo";
        }
    }

    public class HtmlSearchButton : HtmlButton
    {
        public HtmlSearchButton()
        {
            this.Id = "btnSearch";
            this.Class = "btn btn-default btn-xs";
            this.Icon = "icon-search";
            this.Text = "Pesquisar";
        }
    }
    public class HtmlResetButton : HtmlButton
    {
        public HtmlResetButton()
        {
            this.Id = "btnReset";
            this.Class = "btn btn-success";
            this.DataToggle = "";
            this.DataTarget = "";
            this.DataDismiss = "";
            this.Icon = "icon-plus";
            this.Text = "Novo";
        }
    }

    public class HtmlExitButton : HtmlButton
    {
        public HtmlExitButton(string _id, string _dataDismiss, string _dataToggle,string _text = "Sair")
        {
            this.Id = _id;
            this.Class = "btn btn-default";
            this.DataToggle = _dataToggle;
            this.DataTarget = "";
            this.DataDismiss = _dataDismiss;
            this.Icon = "icon-external-link";
            this.Text = _text;
        }

        public HtmlExitButton()
            : this("btnExit", "modal", "modal")
        { }
    }

    public class HtmlSaveButton : HtmlButton
    {
        public HtmlSaveButton()
        {
            this.Id = "btnSave";
            this.Class = "btn btn-primary";
            this.Icon = "icon-save";
            this.Text = "Salvar";
        }
    }

    public class HtmlConfirmButton : HtmlButton
    {
        public HtmlConfirmButton()
        {
            this.Id = "btnConfirm";
            this.Class = "btn btn-primary";
            this.Icon = "icon-check";
            this.Text = "Confirmar";
        }
    }

    public class HtmlPanelButtom : HtmlButton
    {
        public HtmlPanelButtom()
        {
            this.Id = "btnPanel";
            this.ButtonSize = ButtonSize.extra_small;
            this.HorizontalAlign = ButtonHorizontalAlign.right;
            this.Icon = "icon-plus";
        }
    }
}
