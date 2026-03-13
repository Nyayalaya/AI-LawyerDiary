using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class GetCaseManagmentResponse
    {
        public Guid Id { get; set; }
        public DateTime InstitutionDate { get; set; }
        public Guid ClientId { get; set; }
        public Guid NatureId { get; set; }
        public Guid TypeCaseId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CaseTypeId { get; set; }
        public string Number { get; set; }
        public int CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public int TitleTypeFirst { get; set; }
        public string SecondTitle { get; set; }
        public int TitleTypeSecond { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public List<CaseDetailAgainstEntity> AgainstCaseDetails { get; set; }
    }
}
