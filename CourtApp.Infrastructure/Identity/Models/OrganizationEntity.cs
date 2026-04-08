using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_organization", Schema = "Identity")]
    public class OrganizationEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; } 
        public ICollection<UserOrganizationMapping> Users { get; set; }
    }
}
