using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CaseTitle
{
    public class CaseTitleResponse
    {
        public Guid Id { get; set; }        
        public string CaseDetail { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }

        public string CourtType { get; set; }
        public string Court { get; set; }
        public string CaseType { get; set; }
        public string No { get; set; }
        public string Year { get; set; }
        public List<ApplicantDetailDto> CaseApplicantDetails { get; set; }
    }
    
}
