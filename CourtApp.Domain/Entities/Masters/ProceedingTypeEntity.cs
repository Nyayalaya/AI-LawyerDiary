using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_proceeding_type", Schema = "masters")]
    public class ProceedingTypeEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
