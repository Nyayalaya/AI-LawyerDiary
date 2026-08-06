using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Providers.Interfaces
{
    public interface IAIProvider
    {
        string Name { get; }

        bool SupportsStreaming { get; }

        bool SupportsFunctionCalling { get; }

        bool SupportsVision { get; }

        bool SupportsEmbeddings { get; }

        bool IsAvailable();
    }
}
