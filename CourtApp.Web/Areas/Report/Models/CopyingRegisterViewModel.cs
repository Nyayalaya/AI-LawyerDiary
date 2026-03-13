using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Report.Models
{
    public class CopyingRegisterViewModel
    {
        public SelectList Filters { get; set; }
        public List<CopyingCaseDetailModel> copyingCases { get; set; }
    }
    public class CopyingCaseDetailModel: CaseDetailViewModel
    {       
        public bool Selected { get; set; }
        public string AppliedOn { get; set; }
        public string ReceivedOn { get; set; }
    }
}
