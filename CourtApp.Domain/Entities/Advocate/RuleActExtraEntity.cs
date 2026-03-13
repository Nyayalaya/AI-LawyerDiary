using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.rule_act_extra")]
    public class RuleActExtraEntity: AuditableEntity
    {
        public int RuleId { get; set; }
        public int ActId { get; set; }
    }
}
