using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_professional", Schema = "Identity")]
    public class ProfessionalInfoEntity:AuditableEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string EnrollmentNo { get; set; }
        public string BarCouncil { get; set; }
        public int? ExperienceYears { get; set; }
    }
}
