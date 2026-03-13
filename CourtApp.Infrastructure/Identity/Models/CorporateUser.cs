using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Infrastructure.Identity.Models
{
    public class CorporateUser
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string FirmName { get; set; }
        public string FirmType { get; set; }
        public string Address { get; set; }
        public string RegistrationNo { get; set; }
        public string Owner { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
