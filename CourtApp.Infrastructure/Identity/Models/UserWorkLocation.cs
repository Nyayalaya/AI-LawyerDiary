using AuditTrail.Abstrations;
using System;

using System.ComponentModel.DataAnnotations.Schema;


namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_work_location")]
    public class UserWorkLocation:AuditableEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign Keys only - no navigation properties (court entities are in ApplicationDbContext)
        public Guid CourtId { get; set; }

        public Guid? CourtComplexId { get; set; }
        public Guid? CourtHallId { get; set; }

        public bool IsPrimary { get; set; }
    }
}
