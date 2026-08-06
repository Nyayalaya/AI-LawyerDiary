using System;

namespace CourtApp.Application.Features.Court.DTOs
{
    public class CourtResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
