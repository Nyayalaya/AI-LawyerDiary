using System;

namespace CourtApp.Application.Features.FormManagement.DTOs
{
    public class FormSubtypeDto
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateFormSubtypeDto
    {
        public Guid FormId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class UpdateFormSubtypeDto
    {
        public Guid FormId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class FormSubtypeResponseDto
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public string FormName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
