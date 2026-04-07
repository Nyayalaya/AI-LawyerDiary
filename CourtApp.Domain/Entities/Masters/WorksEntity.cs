using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_work")]
    public class WorksEntity : AuditableEntity
    {           
        public Guid CourtTypeId { get; set; }
        public required string Name { get; set; }
        public string Code { get; set; }
        public Guid WorkId { get; set; }
        public virtual WorkTypeEntity Work { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }

    }
}
