using System;
namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CourtMasterListModel
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Complex { get; set; }
        public string CourtType { get; set; }
        public string CourtName { get; set; }
    }
}
