using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_work_location", Schema = "Identity")]
    public class UserWorkLocation:AuditableEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid CourtId { get; set; }
        public CourtEntity Court { get; set; }

        public Guid? CourtComplexId { get; set; }
        public Guid? CourtHallId { get; set; }

        public bool IsPrimary { get; set; }
    }
}
