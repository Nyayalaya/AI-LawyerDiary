using System;
using System.ComponentModel.DataAnnotations;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class ProceedingHeadViewModel
    {
        public Guid Id { get; set; }        
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
}
