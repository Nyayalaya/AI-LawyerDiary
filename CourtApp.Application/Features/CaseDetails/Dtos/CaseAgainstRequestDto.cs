using System;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    public class CaseAgainstRequestDto
    {
        public DateTime ImpugedOrderDate { get; set; }
        public Guid CourtLevelId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? CourtComplexId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CourtHallId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public int StateId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public string CisNumber { get; set; }
        public int? CisYear { get; set; }
        public string CnrNumber { get; set; }
        public string OfficerName { get; set; }
        public Guid? CadreId { get; set; }
    }
}
