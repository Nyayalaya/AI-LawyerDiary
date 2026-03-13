using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{
    [Table("case_assigned", Schema = "ld")]
    public class AssignCaseEntity : AuditableEntity
    {
        public Guid CaseId { get; set; }
        public Guid LawyerId { get; set; }
        public virtual CaseDetailEntity Case { get; set; }
    }
}
