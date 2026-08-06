using MediatR;
using Microsoft.Extensions.Logging;

namespace CourtApp.Infrastructure.AI.Plugins.DocumentPlugin
{
    public sealed partial class DocumentPlugin:PluginBase
    {
        public DocumentPlugin(IMediator mediator,ILogger<DocumentPlugin> logger)
        : base(mediator, logger)
        {
        }
    }
}
