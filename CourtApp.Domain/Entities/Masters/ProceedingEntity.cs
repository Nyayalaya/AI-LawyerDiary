using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_proceeding", Schema = "masters")]
    public class ProceedingEntity : AuditableEntity
    {          
        public Guid ProceedingTypeId { get; set; } 
        public required string Name { get; set; }
        public string Code { get; set; }        
        public virtual ProceedingTypeEntity ProceedingType { get; set; }
    }
}
