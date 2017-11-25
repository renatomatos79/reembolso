using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using HackathonReembolso.Framework.Mvc.Components.Html;
using InputType = HackathonReembolso.Framework.Mvc.Components.Html.InputType;

namespace HackathonReembolso.Framework.Mvc.Components.Navigator
{
    public static class PageHelper
    {
        public static HtmlString Pagination(this HtmlHelper helper, string id)
        {
            return Pagination(helper, id, new List<HtmlButton>());
        }

        public static HtmlString Pagination(this HtmlHelper helper, string id, List<HtmlButton> buttons)
        {
            #region Inicialização
            
            // opções de paginação do DropDown
            var options = new int[] { 10, 20, 30, 40, 50, 100 };

            #endregion Inicialização

            var text = new System.Text.StringBuilder();

            text.AppendLine(string.Format("<div id='{0}' name='{0}' class='row'>", id));
            text.AppendLine("    <div class='col-md-12' style='background-color: #f5f5f5; height: 40px; padding: 7px 0 0 0'>");
            
            #region Adiciona os botões incluir e pesquisar
            
            text.AppendLine("        <div class='col-md-4'>");

            foreach (var but in buttons)
            {
                text.AppendLine(but.ToString());
            }

            text.AppendLine("        </div>");

            #endregion Adiciona os botões a barra de paginação

            #region Botões de navegação

            var btnFirst = new HtmlButton() { Id = "btnFirst", Class = "btn btn-default btn-xs", Icon = "icon-double-angle-left", Title = "Primeira página"}; 
            var btnPrev = new HtmlButton() { Id = "btnPrev", Class = "btn btn-default btn-xs", Icon = "icon-angle-left", Title = "Página anterior"};
            var inputPage = new HtmlInput() { Id = "txtPage", Class = "ui-pg-input", Type = InputType.text, Role = "textbox", Size = 2, MaxLenght = 4};
            var btnNext = new HtmlButton() { Id = "btnNext", Class = "btn btn-default btn-xs", Icon = "icon-angle-right", Title = "Próxima página"};
            var btnLast = new HtmlButton() { Id = "btnLast", Class = "btn btn-default btn-xs", Icon = "icon-double-angle-right", Title = "Última página"};

            #endregion Botões de navegação
            
            text.AppendLine("        <div class='col-md-6' style='margin-top: -3px'>");
            text.AppendLine(btnFirst.ToString());
            text.AppendLine(btnPrev.ToString());
            text.AppendLine(String.Format(" | Página {0} de <span id='spnPages'></span> | ", inputPage.ToString())); 
            text.AppendLine(btnNext.ToString());
            text.AppendLine(btnLast.ToString());
            text.AppendLine("            <select id='cbxPages' class='ui-pg-selbox' role='listbox'></select>");
            text.AppendLine("        </div>");
            text.AppendLine("        <div class='col-md-2' style='text-align:right'>");
            text.AppendLine("           Linhas: <span id='spnFirstRecord'></span> - <span id='spnLastRecord'></span> de <span id='spnRecords'></span>");
            text.AppendLine("        </div>");
            text.AppendLine("    </div>");
            text.AppendLine("</div>");

            return new HtmlString(text.ToString());
        }
    }
}
