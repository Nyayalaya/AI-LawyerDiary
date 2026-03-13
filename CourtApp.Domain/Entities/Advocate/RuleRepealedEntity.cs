using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.m_repealed_rule")]
    public class RuleRepealedEntity : AuditableEntity
    {
        [ForeignKey("Rule")]
        public int RuleId { get; set; }
        public int RepealedRuleID { get; set; }
    }
}
