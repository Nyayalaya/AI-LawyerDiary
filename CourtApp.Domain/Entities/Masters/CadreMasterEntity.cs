using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_cadre")]
    [Index(nameof(Code), IsUnique = true)]
    public class CadreMasterEntity : AuditableEntity
    {
        public required string Name { get; set; }
        public string Code { get; set; }
    }
}
