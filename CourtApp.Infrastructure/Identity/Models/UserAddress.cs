using AuditTrail.Abstrations;
using CourtApp.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_adress")]
    public class UserAddress:AuditableEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public int StateId { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }

        public AddressType Type { get; set; }

        public bool IsPrimary { get; set; }   // ⭐ Important
    }
}
