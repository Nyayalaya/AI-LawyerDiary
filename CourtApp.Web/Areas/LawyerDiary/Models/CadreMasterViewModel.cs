using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CadreMasterViewModel
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Message { get; set; }
    }
}
