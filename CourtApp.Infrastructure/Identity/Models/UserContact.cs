using AuditTrail.Abstrations;
using CourtApp.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_contact")]
    public class UserContact:AuditableEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ContactType ContactType { get; set; }

        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public bool IsPrimary { get; set; }

    }
}
