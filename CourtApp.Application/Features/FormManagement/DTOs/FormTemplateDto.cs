using System;

namespace CourtApp.Application.Features.FormManagement.DTOs
{
    public class FormTemplateDto
    {
        public Guid Id { get; set; }
        public Guid FormSubtypeId { get; set; }
        public string Title { get; set; }
        public string TemplateContent { get; set; }
        public bool IsEditable { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }
        public string CaseTypeCode { get; set; }
        public string StateCode { get; set; }
    }

    public class CreateFormTemplateDto
    {
        public Guid FormSubtypeId { get; set; }
        public string Title { get; set; }
        public string TemplateContent { get; set; }
        public bool IsEditable { get; set; } = true;
        public string Version { get; set; }
        public string CaseTypeCode { get; set; }
        public string StateCode { get; set; }
    }

    public class UpdateFormTemplateDto
    {
        public Guid FormSubtypeId { get; set; }
        public string Title { get; set; }
        public string TemplateContent { get; set; }
        public bool IsEditable { get; set; }
        public string Version { get; set; }
        public string CaseTypeCode { get; set; }
        public string StateCode { get; set; }
    }

    public class FormTemplateResponseDto
    {
        public Guid Id { get; set; }
        public Guid FormSubtypeId { get; set; }
        public string FormSubtypeName { get; set; }
        public string Title { get; set; }
        public string TemplateContent { get; set; }
        public bool IsEditable { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }
        public string CaseTypeCode { get; set; }
        public string StateCode { get; set; }
    }
}
