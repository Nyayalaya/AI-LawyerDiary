using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_work_master_sub", Schema = "ld")]
    public class WorkMasterSubEntity : AuditableEntity
    {
           
        public Guid WorkId { get; set; }        
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public virtual WorkMasterEntity Work { get; set; }
    }
}
