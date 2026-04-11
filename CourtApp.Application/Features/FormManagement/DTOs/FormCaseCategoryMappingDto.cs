using System;

namespace CourtApp.Application.Features.FormManagement.DTOs
{
    public class FormCaseCategoryMappingDto
    {
        public Guid Id { get; set; }
        public Guid FormSubtypeId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateFormCaseCategoryMappingDto
    {
        public Guid FormSubtypeId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public bool IsMandatory { get; set; } = false;
    }

    public class UpdateFormCaseCategoryMappingDto
    {
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
    }

    public class FormCaseCategoryMappingResponseDto
    {
        public Guid Id { get; set; }
        public Guid FormSubtypeId { get; set; }
        public string FormSubtypeName { get; set; }
        public Guid CaseCategoryId { get; set; }
        public string CaseCategoryName { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
    }
}
