using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("client", Schema = "ld")]
    public class ClientEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string ReferalBy { get; set; }
        public string RegNo { get; set; }
        public string Properiter { get; set; }
        public string ClientType { get; set; }
    }
}