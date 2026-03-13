using CourtApp.Application.DTOs.CaseTitle;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CaseTitleRes
{
    public class CaseTitleByIdResponse
    {
        public  Guid Id { get; set; }
        public  Guid CaseId { get; set; }
        public int TypeId { get; set; }
        public  string Title { get; set; }
        public List<ApplicantDetailDto> CaseApplicants { get; set; }
    }
}
