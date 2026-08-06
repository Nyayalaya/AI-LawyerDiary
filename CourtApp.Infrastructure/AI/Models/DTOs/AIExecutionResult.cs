using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.DTOs
{
    public sealed class AIExecutionResult
    {
        public bool Success { get; set; }

        public string Provider { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public TimeSpan ExecutionTime { get; set; }

        public TokenUsage TokenUsage { get; set; } = new();

        public string? ErrorMessage { get; set; }
    }
}
