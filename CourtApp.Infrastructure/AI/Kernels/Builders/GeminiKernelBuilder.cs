using CourtApp.Infrastructure.AI.Configuration.Core;
using CourtApp.Infrastructure.AI.Constants;
using LawyerDiary.Infrastructure.AI.Kernels.Registration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using System;


namespace CourtApp.Infrastructure.AI.Kernels.Builders
{
    public sealed class GeminiKernelBuilder : IKernelBuilder
    {
        private readonly AIOptions _options;

        private readonly ILogger<GeminiKernelBuilder> _logger;
        private readonly IPluginRegistration _pluginRegistration;

        public GeminiKernelBuilder(
            IOptions<AIOptions> options,
            ILogger<GeminiKernelBuilder> logger,
            IPluginRegistration pluginRegistration)
        {
            _options = options.Value;
            _logger = logger;
            _pluginRegistration = pluginRegistration;
        }

        public string ProviderName => ProviderConstants.Gemini;

        public Kernel Build()
        {
            _logger.LogInformation(
            "Creating Gemini Kernel using model {Model}",
            _options.Gemini.Model);

            ValidateConfiguration();

            var builder = Kernel.CreateBuilder();

            builder.AddGoogleAIGeminiChatCompletion(
                modelId: _options.Gemini.Model,
                apiKey: _options.Gemini.ApiKey);

            var kernel = builder.Build();

            _pluginRegistration.RegisterPlugins(kernel);

            return kernel;
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(_options.Gemini.ApiKey))
                throw new InvalidOperationException("Gemini API key is missing.");

            if (string.IsNullOrWhiteSpace(_options.Gemini.Model))
                throw new InvalidOperationException("Gemini model is missing.");
        }
    }
}
