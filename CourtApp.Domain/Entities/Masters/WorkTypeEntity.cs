using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_work_type")]
    public class WorkTypeEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
    }
}
