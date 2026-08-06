using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Core
{
    public sealed class FeatureFlagsOptions
    {
        public bool EnableChat { get; set; }
        public bool EnablePlugins { get; set; }
        public bool EnableAgents { get; set; }
        public bool EnableEmbedding { get; set; }
        public bool EnableRag { get; set; }
        public bool EnableVision { get; set; }
    }
}
