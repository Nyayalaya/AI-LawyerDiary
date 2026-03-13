using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class BringCaseViewModel
    {
        public SelectList Lawyers { get; set; }
        public string LawyerId { get; set; }
        public Guid CaseId { get; set; }

    }
}
