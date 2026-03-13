using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Advocate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_subject", Schema = "common")]
    public class SubjectEntity : AuditableEntity
    {       
        public int TypeId { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public virtual ICollection<ActEntity> Acts { get; set; }
    }
}