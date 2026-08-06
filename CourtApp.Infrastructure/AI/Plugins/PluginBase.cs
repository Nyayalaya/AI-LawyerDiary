using CourtApp.Infrastructure.AI.Common;
using CourtApp.Infrastructure.AI.Helpers;
using CourtApp.Infrastructure.AI.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Plugins
{
    public abstract class PluginBase
    {
        protected IMediator Mediator { get; }

        protected ILogger Logger { get; }

        protected PluginBase(
            IMediator mediator,
            ILogger logger)
        {
            Mediator = mediator;
            Logger = logger;
        }

        protected async Task<TResponse> SendAsync<TResponse>(
            IRequest<TResponse> request,
            string action,
            CancellationToken cancellationToken = default)
        {
            try
            {
                PluginLogger.RequestStarted(
                    Logger,
                    GetType().Name,
                    action);

                var response = await Mediator.Send(
                    request,
                    cancellationToken);

                PluginLogger.RequestCompleted(
                    Logger,
                    GetType().Name,
                    action);

                return response;
            }
            catch (Exception ex)
            {
                PluginLogger.Error(
                    Logger,
                    GetType().Name,
                    ex);

                throw new AIPluginException(
                    "PLUGIN_ERROR",
                    ex.Message,
                    ex);
            }
        }
    }
}
