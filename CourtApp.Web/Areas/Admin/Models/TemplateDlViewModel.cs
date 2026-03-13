using System;

namespace CourtApp.Web.Areas.Admin.Models
{
    public class TemplateDlViewModel
    {
        public Guid Id { get; set; }
        public string TemplateName { get; set; }
        public string TemplatePath { get; set; }
    }
}
