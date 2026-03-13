using System;
using System.Collections.Generic;
namespace CourtApp.Application.DTOs.FormBuilder
{
    public class GetTemplateInfoDtoResponse
    {
        public Guid Id { get; set; }
        public string TemplateName { get; set; }
        public string TemplatePath { get; set; }
        public List<TemplateTagInfo> Tags { get; set; }
    }

    public class TemplateTagInfo
    {
        public string Tag { get; set; }
    }
}
