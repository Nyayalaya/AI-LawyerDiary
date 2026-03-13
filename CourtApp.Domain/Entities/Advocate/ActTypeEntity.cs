using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("m_act_type",Schema ="ad")]
    public class ActTypeEntity: AuditableEntity
    {
        public new Guid Id { get; set; }        
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
