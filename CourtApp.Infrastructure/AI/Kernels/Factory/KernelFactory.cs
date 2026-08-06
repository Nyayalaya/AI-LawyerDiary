using CourtApp.Infrastructure.AI.Configuration.Core;
using CourtApp.Infrastructure.AI.Kernels.Builders;
using LawyerDiary.Infrastructure.AI.Kernels.Factory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CourtApp.Infrastructure.AI.Kernels.Factory
{
    public sealed class KernelFactory : IKernelFactory
    {
        private readonly AIOptions _options;

        private readonly ILogger<KernelFactory> _logger;

        private readonly Dictionary<string, IKernelBuilder> _builders;

        public KernelFactory(
            IEnumerable<IKernelBuilder> builders,
            IOptions<AIOptions> options,
            ILogger<KernelFactory> logger)
        {
            _options = options.Value;
            _logger = logger;

            _builders = builders.ToDictionary(
                x => x.ProviderName,
                StringComparer.OrdinalIgnoreCase);
        }

        public Microsoft.SemanticKernel.Kernel CreateKernel()
        {
            return CreateKernel(
        _options.Provider.CurrentProvider);
        }

        public Microsoft.SemanticKernel.Kernel CreateKernel(string provider)
        {
            if (!_builders.TryGetValue(
            provider,
            out var builder))
            {
                throw new InvalidOperationException(
                    $"Kernel Builder not found for {provider}");
            }

            _logger.LogInformation(
                "Using Kernel Builder {Provider}",
                provider);

            return builder.Build();
        }
    }
}
