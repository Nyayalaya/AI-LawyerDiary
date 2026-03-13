using System;

namespace CourtApp.Web.Areas.Report.Models
{
    public class SearchModel
    {
        public Guid ClientId { get; set; }
        public string ReferalBy { get; set; }
        public string Status { get; set; }
    }
}
