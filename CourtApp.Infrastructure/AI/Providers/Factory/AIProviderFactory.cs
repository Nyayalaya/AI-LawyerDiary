using CourtApp.Infrastructure.AI.Configuration.Core;
using CourtApp.Infrastructure.AI.Providers.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourtApp.Infrastructure.AI.Providers.Factory;

public sealed class AIProviderFactory : IAIProviderFactory
{
    private readonly AIOptions _options;

    private readonly ILogger<AIProviderFactory> _logger;

    private readonly Dictionary<string, IChatProvider> _providers;

    public AIProviderFactory(
        IEnumerable<IChatProvider> providers,
        IOptions<AIOptions> options,
        ILogger<AIProviderFactory> logger)
    {
        _options = options.Value;
        _logger = logger;

        _providers = providers.ToDictionary(
            p => p.Name,
            StringComparer.OrdinalIgnoreCase);
    }

    public IChatProvider GetCurrentProvider()
    {
        return GetProvider(_options.Provider.CurrentProvider);
    }

    public IChatProvider GetProvider(string providerName)
    {
        if (string.IsNullOrWhiteSpace(providerName))
        {
            throw new ArgumentNullException(nameof(providerName));
        }

        if (_providers.TryGetValue(providerName, out var provider))
        {
            _logger.LogInformation(
                "Using AI Provider : {Provider}",
                providerName);

            return provider;
        }

        if (_options.Provider.AllowProviderFallback)
        {
            _logger.LogWarning(
                "Provider {Provider} not found. Using fallback provider {Fallback}.",
                providerName,
                _options.Provider.FallbackProvider);

            if (_providers.TryGetValue(
                _options.Provider.FallbackProvider,
                out var fallback))
            {
                return fallback;
            }
        }

        throw new InvalidOperationException(
            $"AI Provider '{providerName}' is not registered.");
    }
}