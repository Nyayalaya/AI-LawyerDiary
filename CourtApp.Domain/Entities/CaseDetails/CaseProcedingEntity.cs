using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Masters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{
    [Table("case_proceedings")]
    public class CaseProcedingEntity : AuditableEntity
    {       
        public Guid CaseId { get; set; }
        public Guid HeadId { get; set; }
        public Guid SubHeadId { get; set; }
        public Guid? StageId { get; set; }
        public DateTime? ProceedingDate { get; set; }
        public DateTime? NextDate { get; set; }
        public ProceedingWorkEntity ProcWork { get; set; }
        public virtual ProceedingTypeEntity Head { get; set; }
        public virtual CaseStageEntity Stage { get; set; }
        public virtual ProceedingEntity SubHead { get; set; }
        public virtual CaseDetailEntity Case { get; set; }

    }
}
