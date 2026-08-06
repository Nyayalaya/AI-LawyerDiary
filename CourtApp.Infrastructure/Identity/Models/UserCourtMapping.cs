using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_court_mapping")]
    [Index(nameof(UserId), nameof(CourtId), nameof(CourtHallId), IsUnique = true)]
    public class UserCourtMapping : AuditableEntity
    {

        // 👤 User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // 🏛️ Court Hierarchy (Foreign Keys only - no navigation properties)
        public Guid CourtId { get; set; }

        public Guid? CourtComplexId { get; set; }

        public Guid? CourtHallId { get; set; }

        // 📍 Location ID only - no navigation property
        public Guid LocationId { get; set; }

        // ⭐ Metadata
        public bool IsPrimary { get; set; } = false;

        public DateTime? PracticeStartDate { get; set; }
        public DateTime? PracticeEndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
