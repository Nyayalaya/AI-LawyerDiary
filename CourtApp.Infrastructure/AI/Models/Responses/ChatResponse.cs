using CourtApp.Infrastructure.AI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.Responses
{
    public sealed class ChatResponse
    {
        public bool Success { get; set; }

        public string Content { get; set; } = string.Empty;

        public string Provider { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public TokenUsage TokenUsage { get; set; } = new();

        public AIExecutionResult Execution { get; set; } = new();

        public IList<ChatMessage>? History { get; set; }

        public Guid? ConversationId { get; set; }

        public DateTime GeneratedOnUtc { get; set; } = DateTime.UtcNow;

        public IDictionary<string, object>? Metadata { get; set; }
    }
}
