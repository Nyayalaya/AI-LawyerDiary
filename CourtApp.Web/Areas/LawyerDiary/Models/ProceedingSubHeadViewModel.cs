using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class ProceedingSubHeadViewModel
    {
        public string Message { get; set; }
        public Guid Id { get; set; }
        public string Head { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public Guid HeadId { get; set; }
        public string PheadName { get; set; }
        public SelectList PHeads { get; set; }
        public List<ProcHead> ProcHeads { get; set; }

    }
    public class ProcHead
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
