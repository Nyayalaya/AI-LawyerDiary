
using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_specialization")]
    public class UserSpecialization:AuditableEntity
    {   
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid SpecializationId { get; set; }
        // Remove Specialization navigation property - it belongs to ApplicationDbContext
    }
}
