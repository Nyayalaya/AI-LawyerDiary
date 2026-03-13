using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("m_gazzet_type",Schema ="ad")]
    public class GazetteTypeEntity : AuditableEntity
    {
        [Key]
        public new Guid Id { get; set; }
        public string Name_En { get; set; }
        public virtual ICollection<PartEntity> Parts { get; set; }
    }
}
