using System;

namespace CourtApp.Application.Features.FormManagement.DTOs
{
    public class FormTemplateVersionDto
    {
        public Guid Id { get; set; }
        public Guid FormTemplateId { get; set; }
        public string Content { get; set; }
        public string Version { get; set; }
        public bool IsPublished { get; set; }
    }

    public class CreateFormTemplateVersionDto
    {
        public Guid FormTemplateId { get; set; }
        public string Content { get; set; }
        public string Version { get; set; }
        public bool IsPublished { get; set; } = false;
    }

    public class UpdateFormTemplateVersionDto
    {
        public string Content { get; set; }
        public string Version { get; set; }
        public bool IsPublished { get; set; }
    }

    public class FormTemplateVersionResponseDto
    {
        public Guid Id { get; set; }
        public Guid FormTemplateId { get; set; }
        public string Content { get; set; }
        public string Version { get; set; }
        public bool IsPublished { get; set; }
    }
}
