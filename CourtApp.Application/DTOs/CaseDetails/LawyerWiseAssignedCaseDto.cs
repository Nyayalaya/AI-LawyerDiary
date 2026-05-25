using CourtApp.Application.DTOs.Case;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CaseDetails
{
    public class LawyerWiseAssignedCaseDto
    {
        public string LawyerId { get; set; }
        public List<CaseDetailResponse> AssignedCaseInfo { get; set; }
    }
}
