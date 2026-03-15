using CourtApp.Worker;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;


var logger = LogManager
    .Setup()
    .LoadConfigurationFromFile("nlog.config")
    .GetCurrentClassLogger();

try
{
    logger.Info("Starting CourtApp Worker...");

    var host = Host.CreateDefaultBuilder(args)
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders(); // remove default logging
        })
        .UseNLog() // add NLog
        .ConfigureServices((context, services) =>
        {
            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(
                    context.Configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfireServer(options =>
            {
                options.WorkerCount = Environment.ProcessorCount * 5;

                options.Queues = new[]
                {
                    "documents",
                    "extraction",
                    "chunking",
                    "embedding",
                    "citation"
                };
            });

            services.AddJobs();
            //services.AddApplicationServices();
        })
        .Build();

    host.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Worker stopped due to exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}