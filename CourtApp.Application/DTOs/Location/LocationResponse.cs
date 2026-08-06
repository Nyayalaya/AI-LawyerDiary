using System;

namespace CourtApp.Application.DTOs.Location
{
    public class LocationResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public int Type { get; set; }
        public Guid? ParentLocationId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
