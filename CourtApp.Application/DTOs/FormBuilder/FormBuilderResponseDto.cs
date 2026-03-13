using System;

namespace CourtApp.Application.DTOs.FormBuilder
{
    public class FormBuilderResponseDto
    {
        public Guid Id { get; set; }
        public string FormName { get; set; }
        public string Fields { get; set; }
    }
}
