
namespace HackathonReembolso.Framework.Mvc.Model
{
    public class VisualizationFieldModel
    {
        public virtual string CodInputType { get; set; }
        public virtual string CodMask { get; set; }
        public virtual DataSourceModel DataSource { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual string FieldDescription { get; set; }
        public virtual string FieldName { get; set; }
        public virtual string ParentFieldName { get; set; }
        public virtual string Value { get; set; }
    }
}
