namespace HackathonReembolso.Mvc.Models
{
    public class BreadCrumbModel 
    {
        public BreadCrumbClass Class { get; set; }
        public BreadCrumbLink Link { get; set; }
    }

    public class BreadCrumbClass
    {
        public string ClassName { get; set; }
        public string Text { get; set; }
    }

    public class BreadCrumbLink
    {
        public string Href { get; set; }
        public string Text { get; set; }
    }
}
