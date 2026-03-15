using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.AI
{
    [Table("ai_conversion_history", Schema = "ai")]
    public class AIConversation:AuditableEntity
    {
        public Guid CaseId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
