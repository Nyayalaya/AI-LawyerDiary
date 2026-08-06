using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    public class CaseRequestDto
    {
        public List<string> LinkedIds { get; set; } = new();
        public string DiaryNumber { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public DateTime? InstitutionDate { get; set; }
        public DateTime? NextDate { get; set; }
        public DateTime? DisposalDate { get; set; }
        public bool IsDisposed { get; set; }
        public Guid FirstTitleId { get; set; }
        public Guid SecondTitleId { get; set; }
        public string CaseFirstTitle { get; set; }
        public string CaseSecondTitle { get; set; }
        public Guid CourtLevelId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? CourtComplexId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CourtHallId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid? CaseStageId { get; set; }
        public Guid? CaseStatusId { get; set; }
        public Guid? PriorityId { get; set; }
        public string Act { get; set; }
        public string Section { get; set; }
        public string PoliceStation { get; set; }
        public string FIRNumber { get; set; }
        public int? FIRYear { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? OpponentClientId { get; set; }
        public bool IsImportant { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsAssigned { get; set; }
        public string Remarks { get; set; }
        public string InternalRemarks { get; set; }
        public Guid? ParentCaseId { get; set; }
        public string CisNumber { get; set; }
        public string CnrNumber { get; set; }
        public List<CaseAgainstRequestDto> AgainstCases { get; set; } = new();
    }
}
