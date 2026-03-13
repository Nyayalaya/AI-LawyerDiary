using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.m_amended_rule")]
    public class RuleAmendedEntity : AuditableEntity
    {
        [ForeignKey("Rule")]
        public int RuleId { get; set; }
        public int AmendedRuleID { get; set; }
    }
}
