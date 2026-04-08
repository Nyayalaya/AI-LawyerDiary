using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_court_mapping", Schema = "Identity")]
    [Index(nameof(UserId), nameof(CourtId), nameof(CourtHallId), IsUnique = true)]
    public class UserCourtMapping : AuditableEntity
    {
        public Guid Id { get; set; }

        // 👤 User
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        // 🏛️ Court Hierarchy
        public Guid CourtId { get; set; }
        public CourtEntity Court { get; set; }

        public Guid? CourtComplexId { get; set; }
        public CourtComplexEntity CourtComplex { get; set; }

        public Guid? CourtHallId { get; set; }
        public CourtHallEntity CourtHall { get; set; }

        // 📍 Redundant but useful for fast filtering
        public Guid LocationId { get; set; }
        public LocationEntity Location { get; set; }

        // ⭐ Metadata
        public bool IsPrimary { get; set; } = false;

        public DateTime? PracticeStartDate { get; set; }
        public DateTime? PracticeEndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
