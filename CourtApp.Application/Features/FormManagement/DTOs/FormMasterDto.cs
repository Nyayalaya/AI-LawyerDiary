using System;

namespace CourtApp.Application.Features.FormManagement.DTOs
{
    public class FormMasterDto
    {
        public Guid Id { get; set; }
        public Guid FormTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateFormMasterDto
    {
        public Guid FormTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class UpdateFormMasterDto
    {
        public Guid FormTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class FormMasterResponseDto
    {
        public Guid Id { get; set; }
        public Guid FormTypeId { get; set; }
        public string FormTypeName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
