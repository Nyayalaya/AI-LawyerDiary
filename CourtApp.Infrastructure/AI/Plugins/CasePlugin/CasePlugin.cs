using MediatR;
using Microsoft.Extensions.Logging;

namespace CourtApp.Infrastructure.AI.Plugins.CasePlugin
{
    public sealed partial class CasePlugin : PluginBase
    {
        public CasePlugin(IMediator mediator,
            ILogger<CasePlugin> logger)
            : base(mediator, logger)
        {
        }
    }
}
