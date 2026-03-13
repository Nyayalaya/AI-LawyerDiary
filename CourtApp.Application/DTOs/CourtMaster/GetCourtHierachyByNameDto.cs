using System;

namespace CourtApp.Application.DTOs.CourtMaster
{
    public class GetCourtHierachyByNameDto
    {
        public Guid Id { get; set; }
        public Guid CourtTypeId { get; set; }
        public string CourtAbbreviation { get; set; }
        public Guid CourtDistrictId { get; set; }
        public Guid CourtComplexId { get; set; }
        public Guid CourtId { get; set; }

    }
}
