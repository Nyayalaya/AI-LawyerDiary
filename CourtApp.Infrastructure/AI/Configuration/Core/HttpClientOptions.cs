using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Core
{
    public sealed class HttpClientOptions
    {
        public int TimeoutSeconds { get; set; }
        public int MaxConnectionsPerServer { get; set; }
        public bool EnableCompression { get; set; }
    }
}
