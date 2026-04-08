using CourtApp.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Enums
{
    [Table("m_user_hierarchy", Schema = "Identity")]
    [Index(nameof(ChildUserId), IsUnique = true)]
    public class UserHierarchy
    {
        public Guid Id { get; set; }

        public Guid ParentUserId { get; set; }   // Lawyer
        public ApplicationUser ParentUser { get; set; }

        public Guid ChildUserId { get; set; }    // Operator / Clerk / Associate
        public ApplicationUser ChildUser { get; set; }

        public UserRelationType RelationType { get; set; }

        public bool IsActive { get; set; }
    }
}
