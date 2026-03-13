using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{
    [Table("r_case_proceeding", Schema = "ld")]
    public class CaseProcedingEntity : AuditableEntity
    {       
        public Guid CaseId { get; set; }
        public Guid HeadId { get; set; }
        public Guid SubHeadId { get; set; }
        public Guid? StageId { get; set; }
        public DateTime? ProceedingDate { get; set; }
        public DateTime? NextDate { get; set; }
        public ProceedingWorkEntity ProcWork { get; set; }
        public virtual ProceedingHeadEntity Head { get; set; }
        public virtual CaseStageEntity Stage { get; set; }
        public virtual ProceedingSubHeadEntity SubHead { get; set; }
        public virtual CaseDetailEntity Case { get; set; }

    }
}
