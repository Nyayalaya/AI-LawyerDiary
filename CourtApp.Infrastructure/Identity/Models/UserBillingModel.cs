using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_billing_info", Schema = "Identity")]
    public class UserBillingModel:AuditableEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string Branch { get; set; }
        public string Pan { get; set; }
        public string GstNo { get; set; }
    }
}
