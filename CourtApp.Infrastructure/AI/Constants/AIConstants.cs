using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Constants
{
    public static class AIConstants
    {
        public const string ConfigurationSection = "AI";

        public const string HttpClientName = "AIHttpClient";

        public const string KernelName = "LawyerKernel";

        public const string DefaultCulture = "en-US";

        public const string ConversationCacheKey = "AI:Conversation";

        public const string EmbeddingCacheKey = "AI:Embedding";

        public const string PromptCacheKey = "AI:Prompt";

        public const string AgentCacheKey = "AI:Agent";

        public const int DefaultMaxHistory = 20;

        public const int MaxPromptLength = 50000;

        public const int MaxInputTokens = 100000;

        public const int MaxOutputTokens = 8192;
    }
}
