using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("lawerusers", Schema = "Identity")]
    public class OperatorUser : AuditableEntity
    {
        /// <summary>
        /// Foreign  key to applicaition user.
        /// </summary>
        public string LawyerId { get; set; }
        public string Enrollment { get; set; }
        public ApplicationUser Lawyer { get; set; }
        public DateTime DateOfJoining { get; set; }
        public AddressInfo AddressInfo { get; set; }

    }
}
