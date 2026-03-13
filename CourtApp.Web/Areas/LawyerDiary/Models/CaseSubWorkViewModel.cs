using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CaseSubWorkViewModel
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public string WorkName { get; set; }
        public SelectList CWorks { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
