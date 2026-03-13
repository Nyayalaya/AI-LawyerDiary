using System.ComponentModel.DataAnnotations;

namespace CourtApp.Web.Areas.Identity.Models
{
    public abstract class RegisterBaseModel
    {
        [Required]
        public string RegistrationNo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        public string Mobile { get; set; }

        [Display(Name = "Telphone")]
        public string Telephone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
