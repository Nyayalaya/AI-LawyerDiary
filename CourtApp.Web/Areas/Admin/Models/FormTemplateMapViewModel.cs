using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Admin.Models
{
    public class FormTemplateMapViewModel
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public SelectList Forms { get; set; }
        public Guid FormId { get; set; }
        public List<Mapping> Tags { get; set; }
    }
    public class Mapping
    {
        public string Tag { get; set; }       
        public Guid Key { get; set; }       
    }
}
