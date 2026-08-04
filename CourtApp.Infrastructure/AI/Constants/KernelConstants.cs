using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Constants
{
    public static class KernelConstants
    {
        public const string KernelName = "LawyerDiaryKernel";

        public const string ChatService = "ChatCompletion";

        public const string EmbeddingService = "Embedding";

        public const string MemoryService = "Memory";

        public const string Planner = "Planner";

        public const string PromptDirectory = "PromptTemplates";

        public const string PluginDirectory = "Plugins";
    }
}
