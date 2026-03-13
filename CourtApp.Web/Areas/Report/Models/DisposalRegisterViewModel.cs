using System;

namespace CourtApp.Web.Areas.Report.Models
{
    public class DisposalRegisterViewModel: CaseDetailViewModel
    {       
        public string Reason { get; set; }
        public string DisposalDate { get; set; }
    }
}
