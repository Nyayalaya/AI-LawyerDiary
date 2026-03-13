using System.ComponentModel.DataAnnotations;

namespace CourtApp.Web.Areas.Identity.Models
{
    public class RegisterViewModel : RegisterBaseModel
    {
        [Required]
        public string UserType { get; set; }

        // Lawyer properties
        public LawyerModel Lawyer { get; set; }

        // Corporate properties
        //public CorporateModel Corporate { get; set; }
    }
}
