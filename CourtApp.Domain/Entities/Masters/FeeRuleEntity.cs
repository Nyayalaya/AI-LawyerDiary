using AuditTrail.Abstrations;
using System;

namespace CourtApp.Domain.Entities.Masters
{
    public class FeeRuleEntity:AuditableEntity
    {
        public int StateId { get; set; }
        public Guid CourtLevelId { get; set; }
        public Guid CaseTypeId { get; set; }
        public int CalculationType { get; set; }
        public decimal? Cap { get; set; }
        public decimal? MinFee { get; set; }
        public Slab Slab { get; set; }
    }
    public class Slab
    {
        public decimal Limit { get; set; }
        public decimal Rate { get; set; }
    }
}
