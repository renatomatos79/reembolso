using HackathonReembolso.Framework.Mvc.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HackathonReembolso.Framework.Helpers;

namespace HackathonReembolso.Framework.Mvc.Components.Html
{
    public class HtmlTable : Html
    {
        public HtmlTable()
        {
            this.Id = "tabData";
            this.Responsive = true;
        }

        private List<HtmlField> _HtmlFields = new List<HtmlField>();
        public virtual List<HtmlField> HtmlFields
        {
            get
            {
                return _HtmlFields;
            }
        }

        public virtual Pagging Pagging { get; set; }
        public virtual IAddButton AddButton { get; set; }
        public virtual bool EnableAddButton { get; set; }
        public virtual bool EnableSearch { get; set; }
        public virtual Loading Loading { get; set; }
        public virtual string ReloadTableDatabind { get; set; }
        public virtual HtmlAdvancedSearch HtmlAdvancedSearch { get; set; }
        public virtual bool Responsive { get; set; }

        public virtual HtmlField AddField<T>(string ColumnTitle, string DataBind, int Width, bool Sortable = false, string ColumnNameBD = "", bool EnableCheckAll = false) where T : HtmlField
        {
            var c = (HtmlField)Activator.CreateInstance(typeof(T), this);
            c.ColumnTitle = ColumnTitle;
            c.DataBind = DataBind;
            c.Width = Width;
            c.Sortable = Sortable;
            c.EnableCheckAll = EnableCheckAll;
            c.ColumnNameBD = ColumnNameBD;           
            HtmlFields.Add(c);

            return c;
        }

        public virtual HtmlField AddField<T>() where T : HtmlField
        {
            return AddField<T>("", "", 1);
        }
        
        public override string ToString()
        {
            var table = new StringBuilder();
            var loadingClass = (Loading == null || Loading.Class == null) ? "col-md-3 col-md-offset-4" : Loading.Class;

            var loadingDatabindProperty = (Loading == null || Loading.DatabindProperty == null) ? "ListLoaded()" : Loading.DatabindProperty;

            #region Loading Dialog

            table.AppendLine("<div id='loading' class='" + loadingClass + "' style='margin-top: 25px;' data-bind='visible: !listLoaded()'>");
            table.AppendLine("  <div class='widget-body'>");
            table.AppendLine("      <img src='" + HelperConstants.Cdn + "/images/loading.gif' style='width: 50px; height: 50px;'>");
            table.AppendLine("      <span style='margin-left: 12px; color: #737373;'>Carregando...</span>");
            table.AppendLine("  </div>");
            table.AppendLine("</div>");

            #endregion Loading Dialog

            table.AppendLine("<div class='widget' data-bind='visible: listLoaded()' style='display: none;'>");

            var enablePagging = (Pagging != null);
            var reloadTableDatabind = ReloadTableDatabind ?? "click: _loadTable";

            #region Add Button / Search

            table.AppendLine("  <div class='widget-header with-footer'>");

            if (AddButton != null)
            {
                table.AppendLine(AddButton.GetHtmlString());
            }
            else if (EnableAddButton)
            {
                table.AppendLine("      <span class='widget-caption' style='margin-top: 7px'><a class='btn btn-primary btn-xs' href='#' id='btnAdd' data-bind='click: modalCreate'><i class='fa fa-plus'></i> Adicionar</a></span>");
            }

            if (EnableSearch)
            {
                table.AppendLine("      <div class='widget-buttons col-xs-2 pull-right' style='margin-top: 5px'>");
                table.AppendLine("          <div class='input-group input-group-xs'>");
                
                if (this.HtmlAdvancedSearch != null)
                {
                    table.AppendLine(this.HtmlAdvancedSearch.ToString());
                }
                else
                {
                    table.AppendLine("  	        <input type='text' class='form-control' placeholder='Pesquisar' data-bind='value: searchValue, event:{keypress: resetSearchKeyPress}'>");
                    table.AppendLine("  	        <span class='input-group-btn'>");
                    table.AppendLine("  		        <button class='btn btn-default' style='margin-top: 0.25px;' type='button' data-bind='click: resetSearch'>OK</button>");
                    table.AppendLine("  	        </span>");
                }

                table.AppendLine("          </div>");
                table.AppendLine("      </div>");
            }

            if (enablePagging)
            {
                table.AppendLine("      <div class='widget-buttons col-xs-2 pull-right' style='margin-top: 5px'>");
                table.AppendLine("          <div class='input-group input-group-xs' style='float: right;'>");
                table.AppendLine("              <select id='recordsPerPage' data-bind='event: { change: updateRecordsPerPage }' class='form-control' style='border-top-right-radius: 0; border-bottom-right-radius: 0; border-top-left-radius: 0; border-bottom-left-radius: 0;'>");
                Pagging.RecordsPerPage.ForEach(c => table.AppendLine(string.Format("<option value='{0}'>{0} linhas por página</option>", c)));
                table.AppendLine("              </select>");
                table.AppendLine("          </div>");
                table.AppendLine("      </div>");
            }

            table.AppendLine("  </div>");

            #endregion Add Button / Search

            table.AppendLine("  <div class='widget-body'>");

            if (this.Responsive)
            {
                table.AppendLine("      <div class=\"table-responsive\">");
            }

            table.AppendFormat("      <table id='{0}' class='table dataTable table-striped table-bordered table-hover {1}'>", this.Id, this.HtmlAdvancedSearch != null ? "advanced-search" : string.Empty);

            #region Header

            table.AppendLine("      <thead>");
            table.AppendLine("          <tr>");
            HtmlFields.ForEach(c => table.AppendLine(c.HtmlHeader));
            table.AppendLine("          </tr>");
            table.AppendLine("      </thead>");

            #endregion Header

            #region Body

            var tableDatabind = string.IsNullOrEmpty(this.DataBind) ? "table" : this.DataBind; 

            table.AppendLine("      <tbody data-bind='foreach: " + tableDatabind + "'>");
            table.AppendLine("          <tr>");
            HtmlFields.ForEach(c => table.AppendLine(c.HtmlRow));
            table.AppendLine("          </tr>");
            table.AppendLine("      </tbody>");

            #endregion Body

            #region Footer

            table.AppendLine("          <tfoot>");

            table.AppendLine("              <tr data-bind='visible: table().length == 0'>");
            table.AppendLine("                  <td colspan='" + this.HtmlFields.Count + "' style='text-align: center'>");
            table.AppendLine("                      Nenhum registro encontrado");
            table.AppendLine("                      <br/> <input type='button' class='btn btn-primary btn-xs' data-bind='click: _loadTable' style='margin-top: 15px;' value='Reinicie sua busca'>");
            table.AppendLine("                  </td>");
            table.AppendLine("              </tr>");

            //if (enablePagging)
            //{
            //    table.AppendLine("              <tr data-bind='visible: " + this.DataBind + "().length > 0'>");
            //    table.AppendLine("                  <td colspan='3'>");
            //    table.AppendLine("                      <small class='pull-left'>Página: <span data-bind='text: PageNumber'></span> <div data-bind='visible: TotalPages() > 0' style='float: right; margin-left: 3px;'> de <span data-bind='text: TotalPages'></span></div></small>");
            //    table.AppendLine("                  </td>");
            //    table.AppendLine("                  <td colspan='5'>");
            //    table.AppendLine("                      <small class='pull-right'>Ir para a página: <input type='text' id='goToPageNumber' style='width: 50px; padding-left: 5px' maxlength='4' value='1' data-bind='value: NextPage' >");
            //    table.AppendLine("                          <input type='button' id='goToPageButton' class='btn btn-primary btn-xs' data-bind='" + Pagging.GoToPageDatabind + "' value='ok' />");
            //    table.AppendLine("                      </small>");
            //    table.AppendLine("                  </td>");
            //    table.AppendLine("              </tr>");
            //}

            table.AppendLine("          </tfoot>");

            #endregion Footer

            table.AppendLine("      </table>");

            if (this.Responsive)
            {
                table.AppendLine("      </div>");
            }

            if (enablePagging)
            {
                table.AppendLine("      <div class='row'>");
                table.AppendLine("          <div class='col-sm-5'>");
                table.AppendLine("              <div class='dataTables_info' id='data_info' role='status' aria-live='polite'>");
                table.AppendLine("                  Página <input id='goToPageNumber' style='width: 22px;' value='1' type='text' data-bind='event: { &#39;keyup&#39;: goToPage }, value: pageNumber'> ");
                table.AppendLine("                  de <span data-bind='text: totalPages'><span>");
                table.AppendLine("              </div>");
                table.AppendLine("          </div>");
                table.AppendLine("          <div class='col-sm-7'>");
                table.AppendLine("              <div class='dataTables_paginate paging_simple_numbers pull-right' style='display:inline-block;' id='data_paginate'>");
                table.AppendLine("                  <ul class='pagination'>");
                table.AppendLine("                      <li class='paginate_button previous' id='data_previous' data-bind='click: goToFirstPage'>");
                table.AppendLine("                          <a href='javascript:void(0)' title='Primeira página'><i class='fa fa-fast-backward'></i></a>");
                table.AppendLine("                      </li>");
                table.AppendLine("                      <li class='paginate_button previous' id='data_previous' data-bind='click: previousPage'>");
                table.AppendLine("                          <a href='javascript:void(0)' title='Página anterior'><i class='fa fa-step-backward'></i></a>");
                table.AppendLine("                      </li>");
                table.AppendLine("                      <li class='paginate_button next' id='data_next' data-bind='click: nextPage'>");
                table.AppendLine("                          <a href='javascript:void(0)' title='Próxima página'><i class='fa fa-step-forward'></i></a>");
                table.AppendLine("                      </li>");
                table.AppendLine("                      <li class='paginate_button next' id='data_next' data-bind='click: goToLastPage'>");
                table.AppendLine("                          <a href='javascript:void(0)' title='Última página'><i class='fa fa-fast-forward'></i></a>");
                table.AppendLine("                      </li>");
                table.AppendLine("                  </ul>");
                table.AppendLine("              </div>");
                table.AppendLine("          </div>");
                table.AppendLine("      </div>");
            }
            table.AppendLine("  </div>");
            table.AppendLine("</div>");

            return table.ToString();
        }
    }

    public class HtmlField : Html
    {
        public HtmlField(HtmlTable table)
        {
            this._HtmlTable = table;
            this.Width = 1;
        }

        private HtmlTable _HtmlTable = null;
        public virtual HtmlTable HtmlTable
        {
            get
            {
                return _HtmlTable;
            }
        }

        public virtual string ColumnTitle { get; set; }
        public virtual int Width { get; set; }
        public virtual bool Sortable { get; set; }
        public virtual bool EnableCheckAll { get; set; }
        public virtual string ColumnNameBD { get; set; }
        public virtual string WidthClass
        {
            get
            {
                return string.Format("col-xs-{0}", this.Width);
            }
        }
        public virtual string HeaderClass { get; set; }
        public virtual string HtmlHeader
        {
            get
            {
                var css = this.HeaderClass + " " + WidthClass;
                var sortClass = this.Sortable ? " sorting" : string.Empty;
                var sortDataBind = this.Sortable ? "' data-bind='click: sortColumn' data-column-name='" + this.ColumnNameBD + "'" : string.Empty;
                var checkAll = string.Format("<div class='checkbox' style='margin-top: 2px; margin-bottom: 0px'><label><input type='checkbox' onchange='checkAll(this);'><span class='text' style='font-size:13px;font-weight:600;'>Todos</span></label></div>", this.DataBind);

                var text = new StringBuilder();
                text.AppendLine("<th class='" + css + sortClass + "'" + sortDataBind + " > ");

                if (EnableCheckAll)
                    text.AppendLine(checkAll);
                else
                    text.AppendLine(this.ColumnTitle);

                text.AppendLine("</th>");

                return text.ToString();
            }
        }

        public virtual string RowClass { get; set; }
        public virtual string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");
                text.AppendLine("   <span data-bind='text: " + this.DataBind + "'></span>");
                text.AppendLine("</td>");

                return text.ToString();
            }
        }
    }

    public class HtmlBooleanField : HtmlField
    {
        public virtual string TrueValue { get; set; }
        public virtual string FalseValue { get; set; }

        public HtmlBooleanField(HtmlTable table)
            : base(table)
        {
            this.TrueValue = "true";
            this.FalseValue = "false";
        }

        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");
                text.AppendLine("   <span data-bind='if: " + this.DataBind + " == " + this.TrueValue + "' class='label label-success'>Ativo</span>");
                text.AppendLine("   <span data-bind='if: " + this.DataBind + " == " + this.FalseValue + "' class='label label-important'>Inativo</span>");
                text.AppendLine("</td>");

                return text.ToString();
            }
        }
    }

    public class HtmlStatusField : HtmlField
    {
        public HtmlStatusField(HtmlTable table) : base(table) { }

        public virtual List<StatusField> StatusList { get; set; }
        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");

                StatusList.ForEach(v =>
                {
                    text.AppendLine("<!-- ko if: " + this.DataBind + " == '" + v.Value + "' -->");
                    text.AppendLine("   <span class='label label-" + v.Color.ToString().ToLower() + "'>" + v.Text + "</span>");
                    text.AppendLine("<!-- /ko -->");
                });
                text.AppendLine("</td>");

                return text.ToString();
            }
        }
    }

    public class HtmlSiglaArrayField : HtmlField
    {
        public HtmlSiglaArrayField(HtmlTable table) : base(table) { }
        public virtual List<SiglaField> SiglaList { get; set; }
        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + " data-bind='foreach: " + this.DataBind + "'>");

                SiglaList.ForEach(v =>
                {
                    text.AppendLine("<!-- ko if: " + v.TextBind + " == '" + v.Value + "' -->");
                    text.AppendLine("   <span data-bind='text: " + v.TextBind + "' class='label label-" + v.Color.ToString().ToLower() + "'>" + v.Text + "</span>");
                    text.AppendLine("<!-- /ko -->");
                });
                text.AppendLine("</td>");

                return text.ToString();
            }
        }
    }

    public class HtmlTextboxField : HtmlField
    {
        public HtmlTextboxField(HtmlTable table) : base(table) { }
        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");
                text.AppendLine("   <input type='text' data-bind='value: " + this.DataBind + "' class='col-xs-12 col-sm-12'>");
                text.AppendLine("</td>");
                return text.ToString();
            }
        }
    }

    public class HtmlSelectField : HtmlField
    {
        public HtmlSelectField(HtmlTable table) : base(table) { }
        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");
                text.AppendLine("   <select id='" + this.Id + "' class='col-xs-12 col-sm-12' style='padding: 1px;' " + this.DataBind + "></select>");
                text.AppendLine("</td>");
                return text.ToString();
            }
        }
    }

    public class HtmlRadioButtonField : HtmlField
    {
        public HtmlRadioButtonField(HtmlTable table) : base(table) { }

        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");
                text.AppendLine("   <div class='radio" + this.ColumnNameBD + "' style='margin-top: 2px; margin-bottom: 0px'>");
                text.AppendLine("      <label>");
                text.AppendLine(string.Format("<input type='radio' name='" + this.ColumnNameBD + "' {0} />", this.DataBind));
                text.AppendLine("            <span class='text'></span>");
                text.AppendLine("      </label>");
                text.AppendLine("   </div>");
                text.AppendLine("</td>");

                return text.ToString();
            }
        }
    }

    public class HtmlCheckboxField : HtmlField
    {
        public HtmlCheckboxField(HtmlTable table) : base(table) { }

        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);
                var disableCheckbox = this.Disabled ? "onclick='return false;'" : string.Empty;

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");
                text.AppendLine("   <div class='checkbox' style='margin-top: 2px; margin-bottom: 0px'>");
                text.AppendLine("      <label>");
                text.AppendLine("          <input type='checkbox' data-bind='checked: " + this.DataBind + "' " + disableCheckbox + ">");
                text.AppendLine("            <span class='text'></span>");
                text.AppendLine("      </label>");
                text.AppendLine("   </div>");
                text.AppendLine("</td>");

                return text.ToString();
            }
        }
    }

    public class HtmlActionField : HtmlField
    {
        public HtmlActionField(HtmlTable table)
            : base(table)
        {
            this.Actions.Add(new EditActionLink());
            this.Actions.Add(new DeleteActionLink());
            this.Actions.Add(new ViewActionLink());
        }

        private List<ActionLink> _Actions = new List<ActionLink>();
        public virtual List<ActionLink> Actions
        {
            get
            {
                return _Actions;
            }
        }

        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");
                this.Actions.ForEach(a => text.AppendLine(a.ToString()));
                text.AppendLine("</td>");

                return text.ToString();
            }
        }
    }

    #region .: ActionLink :.
    public class ActionLink : Html
    {
        public virtual string Href { get; set; }
        public virtual string Icon { get; set; }

        public override string ToString()
        {
            var href = string.IsNullOrEmpty(this.Href) ? "#" : this.Href;
            return string.Format("<a href='{0}' class='{1}' style='margin-top: 2px !important;' data-bind='{2}'><i class='{3}'></i>{4}</a>", href, this.Class, this.DataBind, this.Icon, this.Title);
        }
    }

    public class HtmlLinkField : HtmlField
    {
        public HtmlLinkField(HtmlTable table) : base(table) { }

        public override string HtmlRow
        {
            get
            {
                var css = string.IsNullOrEmpty(this.RowClass) ? "" : string.Format("class='{0}'", this.RowClass);

                var text = new StringBuilder();
                text.AppendLine("<td " + css + ">");
                text.AppendLine("   <a data-bind='attr: { href: " + this.DataBind + " }' target='_blank'><span data-bind='text: " + this.DataBind + "'></span></a>");
                text.AppendLine("</td>");
                return text.ToString();
            }
        }
    }

    public class EditActionLink : ActionLink
    {
        public EditActionLink()
        {
            this.Title = "Editar";
            this.Class = "btn btn-info btn-xs edit";
            this.Icon = "fa fa-edit";
            this.DataBind = "click: $parent.modalEdit";
        }

        public EditActionLink(string Class, string Icon)
        {
            this.Title = "Editar";
            this.Class = Class + " edit";
            this.Icon = Icon;
            this.DataBind = "click: $parent.modalEdit";
        }
    }

    public class DeleteActionLink : ActionLink
    {
        public DeleteActionLink()
        {
            this.Title = "Excluir";
            this.Class = "btn btn-danger btn-xs delete";
            this.Icon = "fa fa-trash-o";
            this.DataBind = "click: $parent.delete";
        }

        public DeleteActionLink(string Class, string Icon)
        {
            this.Title = "Excluir";
            this.Class = Class + " delete";
            this.Icon = Icon;
            this.DataBind = "click: $parent.delete";
        }
    }

    public class ViewActionLink : ActionLink
    {
        public ViewActionLink()
        {
            this.Title = "Detalhes";
            this.Class = "btn btn-xs btn-success";
            this.Icon = "fa fa-eye";
            this.DataBind = "click: $parent.modalDetails";
        }

        public ViewActionLink(string Title, string Class, string Icon)
        {
            this.Title = Title;
            this.Class = Class + " view";
            this.Icon = Icon;
            this.DataBind = "click: $parent.modalDetails";
        }
    }

    public class DropDownActionLink : ActionLink
    {
        public virtual string ButtonSizeClass { get; private set; }
        public virtual string ButtonColorClass { get; private set; }
        public virtual IEnumerable<DropDownItemActionLink> Links { get; private set; }

        public DropDownActionLink(string title = "Ações", string buttonSizeClass = "btn btn-xs", string buttonColorClass = "btn btn-default", string dataBind = "", DropDownItemActionLink[] links = null)
        {
            this.Title = title;
            this.ButtonSizeClass = buttonSizeClass;
            this.ButtonColorClass = buttonColorClass;
            this.DataBind = dataBind;
            this.Links = links != null ? links.ToList() : new List<DropDownItemActionLink>();
        }

        public override string ToString()
        {
            var html = new StringBuilder();
            html.AppendLine("<div class=\"btn-group\">");
            html.AppendLine($"  <button type = \"button\" class=\"btn {this.ButtonSizeClass} {this.ButtonColorClass} dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\">");
            html.AppendLine($"      <i class=\"fa fa-ellipsis-horizontal\"></i>{this.Title}<i class=\"fa fa-angle-down\"></i>");
            html.AppendLine("  </button>");
            if ((this.Links != null) && (this.Links.Any()))
            {
                html.AppendLine("  <ul class=\"dropdown-menu\">");
                this.Links.ToList().ForEach(lnk =>
                {
                    html.AppendLine(lnk.ToString());
                });
                html.AppendLine("  </ul>");
                html.AppendLine("</div>");
            }

            return html.ToString();
        }
    }

    public class DropDownItemActionLink : ActionLink
    {
        public DropDownItemActionLink(string title = "", string href = "#", string dataBind = "", string buttonIcon = "", string buttonClass = "")
        {
            this.Title = title;
            this.Href = href;
            this.DataBind = dataBind;
            this.Class = buttonClass;
            this.Icon = buttonIcon;
        }

        public override string ToString()
        {
            var html = new StringBuilder();
            html.AppendLine("<li>");
            html.AppendLine($"  <a href=\"{this.Href}\" data-bind=\"{this.DataBind}\" class=\"{this.Class}\" \">");

            if (!string.IsNullOrEmpty(this.Icon))
            {
                html.AppendLine($"  <i class=\"{this.Icon}\"></i>");
            }

            html.AppendLine($"      {this.Title}");
            html.AppendLine("   </a>");
            html.AppendLine("</li>");
            return html.ToString();
        }
    }
    #endregion

    #region .: IAddButton :.
    public class AddButton : IAddButton
    {
        private string _text;
        public virtual string GetHtmlString()
        {
            return "      <span class='widget-caption' style='margin-top: 7px'><a class='btn btn-primary btn-xs' href='#' id='btnAdd' data-bind='"
                + this.Databind
                + "'><i class='fa fa-plus'></i> " + Text + " </a></span>";
        }
        public virtual string Databind { get; set; }
        public virtual string Text
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_text))
                {
                    return "Adicionar";
                }
                else
                {
                    return _text;
                }
            }
            set
            {
                _text = value;
            }
        }
    }

    public class AddDropdownButton : IAddButton
    {
        public AddDropdownButton()
        {
            Buttons = new List<IAddButton>();
        }
        public virtual string Databind { get; set; }
        public virtual List<IAddButton> Buttons { get; set; }
        public virtual string GetHtmlString()
        {
            var html = new StringBuilder();
            html.AppendLine("<span class='widget-caption' style='margin-top: 7px'>");
            html.AppendLine("   <div class='btn-group'>");
            html.AppendLine("       <a class='btn btn-primary btn-xs'data-toggle='dropdown' href='#' id='btnAdd' data-bind='" + this.Databind + "'><i class='fa fa-plus'></i> Adicionar</a>");
            html.AppendLine("       <ul class='dropdown-menu dropdown-primary'>");
            Buttons.ForEach(f => html.AppendLine("<li><a href='#' data-bind='" + f.Databind + "'>" + f.Text + "</a></li>"));
            html.AppendLine("       </ul>");
            html.AppendLine("   </div>");
            html.AppendLine("</span>");

            return html.ToString();
        }
        public virtual string Text { get; set; }
    } 
    #endregion

    public class Pagging
    {
        public virtual List<int> RecordsPerPage { get; set; }
        public virtual string GoToPageDatabind { get; set; }
    }

    public class Search
    {
        public virtual string Databind { get; set; }
    }

    public class Loading
    {
        /// <summary>
        /// Default property is "ListLoaded()"
        /// </summary>
        public virtual string DatabindProperty { get; set; }
        public virtual string Class { get; set; }
    }

    public enum StatusFieldColor
    {
        Info,
        Warning,
        Success,
        Danger
    }

    public struct StatusField
    {
        public StatusField(string value, string text, StatusFieldColor color)
            : this()
        {
            Value = value;
            Text = text;
            Color = color;
        }

        public string Value { get; set; }
        public string Text { get; set; }
        public StatusFieldColor Color { get; set; }
    }

    public struct SiglaField
    {
        public SiglaField(string value, string text, string textBind, StatusFieldColor color)
            : this()
        {
            Value = value;
            Text = text;
            TextBind = textBind;
            Color = color;
        }

        public string Value { get; set; }
        public string Text { get; set; }
        public string TextBind { get; set; }
        public StatusFieldColor Color { get; set; }
    }

    #region .: HtmlAdvancedSearch :.
    public class HtmlAdvancedSearch
    {
        public HtmlAdvancedSearch()
        {
            this.HtmlAdvancedSearchFields = new List<HtmlAdvancedSearchField>();
        }
        public virtual string FormTitle { get; set; }
        public virtual List<HtmlAdvancedSearchField> HtmlAdvancedSearchFields { get; set; }
        public virtual HtmlAdvancedSearchField AddSearchField<T>(string id, string title, int width, string columnDb, string cssStyle = "", string inputType = "text", string cssClass = "") where T : HtmlAdvancedSearchField
        {
            var c = (HtmlAdvancedSearchField)Activator.CreateInstance(typeof(T), this);
            c.Id = id;
            c.Title = title;
            c.Width = width;
            c.InputType = inputType;
            c.Class = cssClass;
            c.CssStyle = cssStyle;
            c.ColumnNameBD = columnDb;

            HtmlAdvancedSearchFields.Add(c);

            return c;
        }
        public virtual HtmlAdvancedSearchField AddSearchFieldDatePicker<T>(string id, string title, int width, string columnDb, string dateFormat = "dd-mm-yyyy", string cssStyle = "", string inputType = "text", string cssClass = "") where T : HtmlAdvancedSearchField
        {
            var c = (HtmlAdvancedSearchFieldDatePicker)Activator.CreateInstance(typeof(T), this);
            c.Id = id;
            c.Title = title;
            c.Width = width;
            c.InputType = inputType;
            c.Class = cssClass;
            c.CssStyle = cssStyle;
            c.DateFormat = dateFormat;
            c.ColumnNameBD = columnDb;

            HtmlAdvancedSearchFields.Add(c);

            return c;
        }
        public override string ToString()
        {
            #region Form

            var form = new StringBuilder();
            form.AppendLine("<div id=\"registration-form\">");
            form.AppendLine("   <form role=\"form\" id=\"formAdvancedSearch\" style=\"margin-bottom:0;\">");
            form.AppendLine("       <div class=\"row\">");
            foreach (var item in this.HtmlAdvancedSearchFields)
            {
                form.AppendLine(item.ToString());
            }
            form.AppendLine("           <div class=\"form-group text-right col-xs-12\" style=\"margin:10px 0 0 0;\">");
            form.AppendLine("               <button type=\"button\" id=\"btnAdvancedClear\" class=\"btn btn-default btn-xs pull-left\">Limpar</button>");
            form.AppendLine("               <button type=\"button\" id=\"btnAdvancedSearchCancel\" class=\"btn btn-danger btn-xs\">Cancelar</button>");
            form.AppendLine("               <button type=\"button\" id=\"btnAdvancedSearch\" class=\"btn btn-primary btn-xs\">Pesquisar</button>");
            form.AppendLine("           </div>"); // <-- /div form-group
            form.AppendLine("       </div>"); // <-- /div row
            form.AppendLine("   </form>");
            form.AppendLine("</div>");
            #endregion

            #region Button
            var button = new StringBuilder();
            button.AppendLine("  	        <span class='input-group-btn'>");
            button.AppendFormat("  		        <button id='btn-customsearch' class='btn btn-sm btn-block dropdown-toggle' type='button' data-container='body' data-titleclass='bordered-blue' style=\"margin-top: 0px\" data-class='dropdown-customsearch' data-toggle='popover' data-placement='bottom' data-title='<strong>{0}</strong>'", this.FormTitle);
            button.AppendFormat("                 data-content='{0}' data-original-title='' title='' aria-describedby='popover827179'>", form.ToString());
            button.AppendLine("                     Pesquisa Avançada");
            button.AppendLine("                     <i class=\"fa fa-angle-down\"></i>");
            button.AppendLine("                 </button>");
            button.AppendLine("  	        </span>");
            #endregion

            return button.ToString();
        }
    }

    public class HtmlAdvancedSearchField : Html
    {
        public HtmlAdvancedSearchField(HtmlAdvancedSearch search)
        {
            this._HtmlAdvancedSearch = search;
        }

        private HtmlAdvancedSearch _HtmlAdvancedSearch = null;
        public virtual HtmlAdvancedSearch HtmlAdvancedSearch
        {
            get
            {
                return _HtmlAdvancedSearch;
            }
        }

        public virtual string ColumnNameBD { get; set; }
        public virtual string InputType { get; set; }
        public virtual string CssStyle { get; set; }
        public virtual int Width { get; set; }
        public virtual string WidthClass
        {
            get
            {
                return string.Format("col-md-{0}", this.Width);
            }
        }
    }

    public class HtmlAdvancedSearchFieldText : HtmlAdvancedSearchField
    {
        public HtmlAdvancedSearchFieldText(HtmlAdvancedSearch search) : base(search)
        {
        }
        public override string ToString()
        {
            var group = new StringBuilder();

            group.AppendFormat("<div class=\"{0}\">", this.WidthClass);
            group.AppendLine("  <div class=\"form-group\">");
            group.AppendFormat("    <label for=\"{0}\">{1}</label>", this.Id, this.Title);
            group.AppendFormat("    <input type=\"text\" id=\"{0}\" name=\"{0}\" class=\"form-control {1}\" style=\"{2}\" value=\"\">", this.Id, this.Class, this.CssStyle);
            group.AppendLine("  </div>");
            group.AppendLine("</div>");

            return group.ToString();
        }
    }

    public class HtmlAdvancedSearchFieldDatePicker : HtmlAdvancedSearchField
    {
        public HtmlAdvancedSearchFieldDatePicker(HtmlAdvancedSearch search) : base(search)
        {
        }
        public string DateFormat { get; set; }
        public override string ToString()
        {
            var group = new StringBuilder();

            group.AppendFormat("<div class=\"{0}\">", this.WidthClass);
            group.AppendFormat("    <div class=\"form-group\">", this.WidthClass);
            group.AppendFormat("        <label for=\"{0}\">{1}</label>", this.Id, this.Title);
            group.AppendLine("          <div class=\"input-group\">");
            group.AppendFormat("            <input type=\"text\" id=\"{0}\" name=\"{0}\" class=\"form-control date-picker DateMask {1}\" style=\"{2}\" data-date-format=\"{3}\" value=\"\">", this.Id, this.Class, this.CssStyle, this.DateFormat);
            group.AppendLine("              <span class=\"input-group-addon\">");
            group.AppendLine("                  <i class=\"fa fa-calendar\"></i>");
            group.AppendLine("              </span>");
            group.AppendLine("          </div>");
            group.AppendLine("  </div>");
            group.AppendLine("</div>");

            return group.ToString();
        }
    } 
    #endregion
}