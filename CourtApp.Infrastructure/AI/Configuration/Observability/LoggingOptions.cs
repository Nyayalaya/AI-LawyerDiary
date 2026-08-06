using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Observability
{
    public sealed class LoggingOptions
    {
        public bool LogPrompt { get; set; }
        public bool LogResponse { get; set; }
        public bool LogExecutionTime { get; set; }
        public bool LogTokenUsage { get; set; }
        public bool LogCost { get; set; }
        public bool LogErrors { get; set; }
    }
}
