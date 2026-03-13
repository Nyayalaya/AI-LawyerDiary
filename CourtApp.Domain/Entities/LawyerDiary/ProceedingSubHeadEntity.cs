using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_proceeding_sub_head", Schema = "ld")]
    public class ProceedingSubHeadEntity : AuditableEntity
    {
               
        public Guid HeadId { get; set; } 
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }        
        public virtual ProceedingHeadEntity Head { get; set; }
    }
}
