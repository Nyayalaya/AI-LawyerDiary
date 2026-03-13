using CourtApp.Application.DTOs.CaseTitle;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.LawyerDiary.Models.Title
{
    public class TitleViewModel
    {
        public Guid Id { get; set; }
        public SelectList Cases { get; set; }
        public Guid CaseId { get; set; }
        public SelectList Types { get; set; }
        public int TypeId { get; set; }
        public List<ApplicantDetailViewModel> CaseApplicants { get; set; }
    }
}
