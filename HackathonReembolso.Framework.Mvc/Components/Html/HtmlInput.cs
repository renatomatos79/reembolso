using System;

namespace HackathonReembolso.Framework.Mvc.Components.Html
{
    public enum InputType { text, number, date }
    public class HtmlInput : Html
    {
        public virtual string PlaceHolder { get; set; }
        public virtual InputType Type { get; set; }
        public virtual bool Required { get; set; }
        public virtual int MaxLenght { get; set; }
        public virtual int Size { get; set; }
        public virtual string Value { get; set; }
        public virtual int MinValue { get; set; }
        public virtual int MaxValue { get; set; }

        public HtmlInput()
        { 
            this.PlaceHolder = string.Empty;
            this.Type = InputType.text;
            this.Required = false;
            this.MaxLenght = 0;
            this.Size = 0;
            this.Value  = string.Empty;
            this.MinValue  = 0;
            this.MaxValue = 0;
        }

        protected virtual string InputTypeToString(InputType type)
        {
            if (type == InputType.date)
            {
                return "date";
            }
            else if (type == InputType.number)
            {
                return "number";
            }
            else
            {
                return "text";
            }
        }

        public override string ToString()
        {
            var htmlText = base.ToString();
            var htmlValue = string.IsNullOrEmpty(this.Value) ? string.Empty : string.Format("value='{0}'", this.Value);
            var htmlMin = this.MinValue <= 0 ? string.Empty : string.Format("min='{0}'", this.MinValue.ToString());
            var htmlMax = this.MaxValue <= 0 ? string.Empty : string.Format("max='{0}'", this.MaxValue.ToString());
            var htmlSize = this.Size <= 0 ? string.Empty : string.Format("size='{0}'", this.Size.ToString());
            var htmlRequired = Required ? "required" : string.Empty;
            var htmlPlaceHolder = string.IsNullOrEmpty(this.PlaceHolder) ? string.Empty : string.Format("placeholder='{0}'", this.PlaceHolder.ToString());
            var htmlMaxLength = this.MaxLenght <= 0 ? string.Empty : string.Format("maxlength='{0}'", this.MaxLenght.ToString()); 

            return String.Format("<input type='{0}' " + htmlText + " " + htmlPlaceHolder + " " + htmlMaxLength + " " + htmlValue + " " + htmlMin + " " + htmlMax + " " + htmlSize + " " + htmlRequired + " />", this.InputTypeToString(this.Type));
        }
    }
}