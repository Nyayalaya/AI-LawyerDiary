using System.ComponentModel.DataAnnotations;

namespace CourtApp.Web.Areas.Identity.Models
{
    public class LawyerModel
    {
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Required]
        //[Display(Name = "Gender")]
        //public string Gender { get; set; }

        //[Required]
        //[Display(Name = "Date Of Birth")]
        //public DateTime DateOfBirth { get; set; }
    }
}
