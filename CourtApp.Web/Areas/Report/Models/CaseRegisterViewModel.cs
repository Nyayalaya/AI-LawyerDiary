using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Report.Models
{
    public class CaseRegisterViewModel
    {
        public List<RegisterDetail> registerDetails { get; set; }
    }
    public class RegisterDetail
    {
        public Guid Id { get; set; }
        public string CaseNo { get; set; }
        public string Year { get; set; }
        public string CaseType { get; set; }
        public string Court { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public bool Selected { get; set; }
        public string AppliedOn { get; set; }
        public string ReceivedOn { get; set; }
    }
}
