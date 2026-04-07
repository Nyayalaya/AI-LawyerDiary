using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Masters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{
    [Table("r_case_working", Schema = "ld")]
    public class CaseWorkEntity : AuditableEntity
    {
        
        public Guid CaseId { get; set; }
        public Guid WorkTypeId { get; set; }
        public Guid WorkId { get; set; }
        public DateTime? WorkingDate { get; set; }
        public DateTime? AppliedOn { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string Abbreviation { get; set; }
        public virtual WorkTypeEntity WorkType { get; set; }
        public virtual WorksEntity Work { get; set; }
        public virtual CaseDetailEntity Case { get; set; }
    }
}
