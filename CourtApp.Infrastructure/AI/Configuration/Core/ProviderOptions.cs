using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Core
{
    public sealed class ProviderOptions
    {
        public string CurrentProvider { get; set; } = string.Empty;
        public string FallbackProvider { get; set; } = string.Empty;
        public bool AllowProviderFallback { get; set; }
    }
}
