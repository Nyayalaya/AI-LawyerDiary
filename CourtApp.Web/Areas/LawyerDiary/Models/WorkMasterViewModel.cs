using System;
using System.ComponentModel.DataAnnotations;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class WorkMasterViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Work_En { get; set; }
        public string Work_Hn { get; set; }
        //public string Abbreviation { get; set; }        
    }
}
