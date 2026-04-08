using CourtApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_org_mapping", Schema = "Identity")]
    public class UserOrganizationMapping
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid OrganizationId { get; set; }
        public OrganizationEntity Organization { get; set; }

        public OrganizationRole Role { get; set; }
    }
}
