using System;

namespace CourtApp.Application.Features.CourtDistrict.DTOs
{
    public class CourtDistrictDropdownResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StateName { get; set; }
    }
}
