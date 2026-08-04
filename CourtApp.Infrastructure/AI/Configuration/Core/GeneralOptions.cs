using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Core
{
    public sealed class GeneralOptions
    {
        public bool EnableStreaming { get; set; }

        public bool EnableCaching { get; set; }

        public bool EnableLogging { get; set; }

        public bool EnableRetry { get; set; }

        public bool EnableFunctionCalling { get; set; }

        public bool EnableTelemetry { get; set; }

        public bool EnablePromptLogging { get; set; }

        public bool EnableResponseLogging { get; set; }

        public bool EnableTokenTracking { get; set; }

        public bool EnableCostTracking { get; set; }

        public double DefaultTemperature { get; set; }

        public int DefaultMaxTokens { get; set; }

        public int MaxConversationMessages { get; set; }
    }
}
