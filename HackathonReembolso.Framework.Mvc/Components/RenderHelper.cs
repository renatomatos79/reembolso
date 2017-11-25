using HackathonReembolso.Framework.Helpers;
using HackathonReembolso.Framework.Mvc.Model;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HackathonReembolso.Framework.Mvc.Components
{
    public static class RenderHelper
    {
        public enum CaptchaType : byte
        {
            Unknown = 0,
            Recaptcha = 1
        }

        public static MvcHtmlString GenerateCaptcha<TModel>(this HtmlHelper<TModel> htmlHelper, CaptchaType type = CaptchaType.Unknown, string callback = "")
        {
            var html = new StringBuilder();

            switch (type)
            {
                case CaptchaType.Recaptcha:
                case CaptchaType.Unknown:
                    {                        
                        html.Append(string.Format("<div class=\"g-recaptcha\" data-sitekey=\"{0}\"></div>", HelperConstants.ApplicationRecaptchaSiteKey));
                        html.Append(string.Format("<script src=\"https://www.google.com/recaptcha/api.js?hl=pt-BR&onload={0}\" async defer></script>", callback));

                        break;
                    }
            }

            return MvcHtmlString.Create(html.ToString());
        }

        public static MvcHtmlString GenerateDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, VisualizationFieldModel model, bool fieldsEnabled = true)
        {
            var inputSource = string.Empty;
            var options = new StringBuilder();

            var disabled = (!fieldsEnabled && model.Disabled) ? "disabled=''" : string.Empty;
            var cdDataSource = (model.DataSource != null) ? "data-cddatasource='" + model.DataSource.CdDataSource + "'" : string.Empty;
            var parent = !string.IsNullOrEmpty(model.ParentFieldName) ? "data-parent='" + model.ParentFieldName + "'" : string.Empty;

            if (model.CodInputType == "HIDDEN")
            {
                inputSource = string.Format("<input type='hidden' id='{0}' name='{0}' value='{1}' />", model.FieldName, model.Value);
                return MvcHtmlString.Create(inputSource);
            }

            switch (model.CodInputType)
            {
                case "TEXTBOX":
                    inputSource = string.Format("<input id='{0}' name='{0}' type='text' class='col-xs-12 col-sm-12 {1}' style='margin-top: 5px;' {2} {3} {4} />", model.FieldName, model.CodMask, disabled, parent, cdDataSource);
                    break;
                case "SELECT":
                    const string optionsCaption = "<option value=''>Selecione...</option>";
                    inputSource = string.Format("<select id='{0}' name='{0}' class='col-xs-12 col-sm-12' {3} {4} {5}>{1}{2}</select>", model.FieldName, optionsCaption, GetHtmlSelectOptions(model), disabled, parent, cdDataSource);
                    break;
                case "MULTISELECT":
                    inputSource = string.Format("<select id=\"{0}\" name=\"{0}\" style=\"width:100%;\" class=\"multiselect\" multiple {1} {2} {3}>{1}</select>", model.FieldName, disabled, parent, cdDataSource);
                    break;
                case "CHECKBOX":
                    var checkbox = new StringBuilder();

                    if (model.DataSource != null && model.DataSource.Items != null)
                    {
                        model.DataSource.Items.ForEach(f =>
                        {
                            checkbox.AppendLine("<div class='checkbox'>");
                            checkbox.AppendLine("   <label>");
                            checkbox.AppendLine(string.Format("<input id='{1}' name='{1}' value='{0}' type='checkbox' {2} />", f.Value, model.FieldName, (fieldsEnabled ? "" : "onclick='return false;'")));
                            checkbox.AppendLine(        string.Format("<span class='text'>{0}</span>", f.Text));
                            checkbox.AppendLine("   </label>");
                            checkbox.AppendLine("</div>");
                        });
                    }
                    else
                    {
                        checkbox.AppendLine("<div class='checkbox'>");
                        checkbox.AppendLine("   <label>");
                        checkbox.AppendLine(string.Format("<input id='{0}' name='{0}' type='checkbox' {1} />", model.FieldName, (fieldsEnabled ? "" : "onclick='return false;'")));
                        checkbox.AppendLine("       <span class='text'></span>");
                        checkbox.AppendLine("   </label>");
                        checkbox.AppendLine("</div>");
                    }
                    
                    inputSource = checkbox.ToString();
                    break;
                case "RADIOBUTTON":
                    var radiobutton = new StringBuilder();

                    if (model.DataSource != null && model.DataSource.Items != null)
                    {
                        model.DataSource.Items.ForEach(f =>
                        {
                            radiobutton.AppendLine("<div class='radio'>");
                            radiobutton.AppendLine("   <label>");
                            radiobutton.AppendLine(string.Format("<input id='{0}' name='{0}' value='{1}' type='radio' {2} />", model.FieldName, f.Value, (fieldsEnabled ? "" : "onclick='return false;'")));
                            radiobutton.AppendLine(string.Format("<span class='text'>{0}</span>", f.Text));
                            radiobutton.AppendLine("   </label>");
                            radiobutton.AppendLine("</div>");
                        });
                    }

                    inputSource = radiobutton.ToString();
                    break;
                case "DATEPICKER":
                    var datepicker = new StringBuilder();
                    datepicker.AppendLine("<div class='input-group'>");
                    datepicker.AppendLine(      string.Format("<input class='form-control date-picker' id='{0}' name='{0}' type='text' data-date-format='dd/mm/yyyy' />", model.FieldName));
                    datepicker.AppendLine("     <span class='input-group-addon'>");
                    datepicker.AppendLine("         <i class='fa fa-calendar'></i>");
                    datepicker.AppendLine("     </span>");
                    datepicker.AppendLine("</div>");

                    inputSource = datepicker.ToString();
                    break;
                case "TEXTAREA":
                    inputSource = string.Format("<textarea rows='5' cols='50' class='form-control {1}' id='{0}' name='{0}' style='resize: none;'></textarea>", model.FieldName, model.CodMask);
                    break;
                case "AUTOCOMPLETE":
                    var autocomplete = new StringBuilder();
                    autocomplete.AppendLine(string.Format("<input id='{0}' name='{0}' type='hidden' />", model.FieldName));
                    autocomplete.AppendLine(string.Format("<input type='text' {0} data-fieldid='{1}' class='autocomplete-dynamic col-xs-12 col-sm-12 {2}' style='margin-top: 5px;' {3} {4}/>", cdDataSource, model.FieldName, model.CodMask, disabled, parent));
                    
                    inputSource = autocomplete.ToString();
                    break;
            }

            var field = new StringBuilder();
            field.AppendLine("<div class='form-group'>");
            field.AppendLine(   string.Format("<label class='col-sm-2 control-label no-padding-right'>{0}</label>", model.FieldDescription));
            field.AppendLine("  <div class='col-sm-10'>");
            field.AppendLine(inputSource);
            field.AppendLine("  </div>");
            field.AppendLine("</div>");

            return MvcHtmlString.Create(field.ToString());
        }

        private static string GetHtmlSelectOptions(VisualizationFieldModel model)
        {
            var options = new StringBuilder();

            if (model.DataSource != null && model.DataSource.Items != null)
                model.DataSource.Items.ForEach(f => options.AppendLine(string.Format("<option value='{0}'>{1}</option>", f.Value, f.Text)));

            return options.ToString();
        }
    }
}
