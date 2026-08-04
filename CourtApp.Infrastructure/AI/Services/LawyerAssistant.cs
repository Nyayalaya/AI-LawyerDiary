using CourtApp.Infrastructure.AI.Configuration.Core;
using CourtApp.Infrastructure.AI.Models.Requests;
using CourtApp.Infrastructure.AI.Models.Responses;
using CourtApp.Infrastructure.AI.Providers.Factory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Services;

public sealed class LawyerAssistant : ILawyerAssistant
{
    private readonly IAIProviderFactory _providerFactory;
    private readonly AIOptions _options;
    private readonly ILogger<LawyerAssistant> _logger;

    public LawyerAssistant(
        IAIProviderFactory providerFactory,
        IOptions<AIOptions> options,
        ILogger<LawyerAssistant> logger)
    {
        _providerFactory = providerFactory;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<ChatResponse> ChatAsync( ChatRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateRequest(request);

        _logger.LogInformation(
            "AI request received. Provider : {Provider}",
            _options.Provider.CurrentProvider);

        var provider = _providerFactory.GetCurrentProvider();

        if (!provider.IsAvailable())
        {
            throw new InvalidOperationException(
                $"{provider.Name} provider is not available.");
        }

        var response = await provider.ChatAsync(
            request,
            cancellationToken);

        response.Provider = provider.Name;

        return response;
    }

    private static void ValidateRequest(ChatRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Prompt))
        {
            throw new ArgumentException(
                "Prompt cannot be empty.",
                nameof(request));
        }
    }
}