using CourtApp.Infrastructure.AI.Configuration.Kernel;
using CourtApp.Infrastructure.AI.Configuration.Providers;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Core
{
    public sealed class AIOptions
    {
        public const string SectionName = "AI";
        public ProviderOptions Provider { get; set; } = new();
        public GeneralOptions General { get; set; } = new();
        public GeminiOptions Gemini { get; set; } = new();
        public RetryOptions Retry { get; set; } = new();
        public HttpClientOptions HttpClient { get; set; } = new();
        public TimeoutOptions Timeout { get; set; } = new();
        public FeatureFlagsOptions FeatureFlags { get; set; } = new();
        public KernelOptions Kernel { get; set; } = new();
        public LoggingOptions Logging { get; set; } = new();
    }
}
