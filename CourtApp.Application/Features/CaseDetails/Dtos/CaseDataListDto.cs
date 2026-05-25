
using System;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    public class CaseDataListDto
    {
        public Guid Id { get; set; }
        public string Court { get; set; }
        public string CaseTitle { get; set; }
        public string CaseNumber { get; set; }
        public string CaseType { get; set; }
        public DateTime? FilingDate { get; set; }
        public DateTime? NextDate { get; set; }
        public string AssignedLawyerId { get; set; }
        public string AssignedLawyerName { get; set; }
        public string Status { get; set; }
        public Guid ParentCaseId { get; set; }
        public bool HasChildCases { get; set; }
    }
}
