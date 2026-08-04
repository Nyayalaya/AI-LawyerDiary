using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Core
{
    public sealed class RetryOptions
    {
        public int MaxRetryCount { get; set; }
        public int RetryDelay { get; set; }
        public bool EnableExponentialBackoff { get; set; }
        public bool EnableCircuitBreaker { get; set; }
        public int CircuitBreakerFailures { get; set; }
        public int CircuitBreakerDurationSeconds { get; set; }
    }
}
