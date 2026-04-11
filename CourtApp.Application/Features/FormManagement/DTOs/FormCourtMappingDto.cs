using System;

namespace CourtApp.Application.Features.FormManagement.DTOs
{
    public class FormCourtMappingDto
    {
        public Guid Id { get; set; }
        public Guid FormSubtypeId { get; set; }
        public Guid CourtTypeId { get; set; }
        public bool IsMandatory { get; set; }
    }

    public class CreateFormCourtMappingDto
    {
        public Guid FormSubtypeId { get; set; }
        public Guid CourtTypeId { get; set; }
        public bool IsMandatory { get; set; } = false;
    }

    public class UpdateFormCourtMappingDto
    {
        public Guid CourtTypeId { get; set; }
        public bool IsMandatory { get; set; }
    }

    public class FormCourtMappingResponseDto
    {
        public Guid Id { get; set; }
        public Guid FormSubtypeId { get; set; }
        public string FormSubtypeName { get; set; }
        public Guid CourtTypeId { get; set; }
        public string CourtTypeName { get; set; }
        public bool IsMandatory { get; set; }
    }
}
