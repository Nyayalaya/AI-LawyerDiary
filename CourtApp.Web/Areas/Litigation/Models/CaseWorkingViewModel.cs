using CourtApp.Application.DTOs.CaseWorking;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseWorkingViewModel
    {
        public SelectList WorkTypes { get; set; }
        //public SelectList Works { get; set; }
        public DateTime? WorkingDate { get; set; }
        public List<ProcWork> Workdt { get; set; }

    }
    public class ProcWork
    {
        public Guid? WorkTypeId { get; set; }
        public SelectList Works { get; set; }
        public Guid? WorkId { get; set; }
    }
}
