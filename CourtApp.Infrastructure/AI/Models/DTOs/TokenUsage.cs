using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.DTOs
{
    public sealed record TokenUsage
    {
        public int PromptTokens { get; set; }

        public int CompletionTokens { get; set; }

        public int TotalTokens { get; set; }

        public decimal EstimatedCost { get; set; }

        public string Currency { get; set; } = "USD";
    }
}
