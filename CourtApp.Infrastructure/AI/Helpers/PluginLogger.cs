using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Helpers
{
    public static class PluginLogger
    {
        public static void RequestStarted(
            ILogger logger,
            string plugin,
            string action)
        {
            logger.LogInformation(
                "[{Plugin}] Started {Action}",
                plugin,
                action);
        }

        public static void RequestCompleted(
            ILogger logger,
            string plugin,
            string action)
        {
            logger.LogInformation(
                "[{Plugin}] Completed {Action}",
                plugin,
                action);
        }

        public static void Error(
            ILogger logger,
            string plugin,
            Exception ex)
        {
            logger.LogError(
                ex,
                "[{Plugin}] Exception",
                plugin);
        }
    }
}
