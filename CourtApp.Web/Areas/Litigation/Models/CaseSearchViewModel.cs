using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseSearchViewModel
    {
        public Guid Id { get; set; }       
        public string Court { get; set; }
        public string CaseType { get; set; }
        public string No { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string NoYear { get; set; }
        public string Title { get; set; }
        public string DocFilePath { get; set; }
    }
}
