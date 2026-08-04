using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.DTOs
{
    public sealed class ChatMessage
    {
        public string Role { get; set; } = ChatRole.User;

        public string Content { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string? Name { get; set; }

        public IDictionary<string, object>? Metadata { get; set; }
    }
}
