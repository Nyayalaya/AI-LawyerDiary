using System;

namespace CourtApp.Application.Features.Court.DTOs
{
    public class CourtByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public Guid CourtTypeId { get; set; }
        public string CourtTypeName { get; set; }
        public Guid CourtLevelId { get; set; }
        public string CourtLevelName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
