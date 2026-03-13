using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class BringTodayCaseViewModel
    {
        public DateTime HearingDate { get; set; }
        public List<HearingViewModel> CaseList { get; set; }
    }
}
