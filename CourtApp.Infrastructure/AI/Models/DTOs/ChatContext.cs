using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.DTOs
{
    public sealed record ChatContext
    {
        public string? UserId { get; set; }

        public string? SessionId { get; set; }

        public string? ConversationId { get; set; }

        public string? TenantId { get; set; }

        public IDictionary<string, string>? Variables { get; set; }

        public IDictionary<string, object>? Metadata { get; set; }
    }
}
