using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Configuration.Core
{
    public sealed class TimeoutOptions
    {
        public int ChatTimeoutSeconds { get; set; }
        public int EmbeddingTimeoutSeconds { get; set; }
        public int ImageTimeoutSeconds { get; set; }
    }
}
