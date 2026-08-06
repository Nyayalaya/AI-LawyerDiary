using CourtApp.Infrastructure.AI.Constants;
using CourtApp.Infrastructure.AI.Models.Requests;
using CourtApp.Infrastructure.AI.Models.Responses;
using CourtApp.Infrastructure.AI.Providers.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Providers.Gemini;

public sealed class GeminiProvider :
    IChatProvider,
    IEmbeddingProvider
{
    private readonly GeminiChatService _chatService;
    private readonly GeminiEmbeddingService _embeddingService;

    public GeminiProvider(
        GeminiChatService chatService,
        GeminiEmbeddingService embeddingService)
    {
        _chatService = chatService;
        _embeddingService = embeddingService;
    }

    public string Name => ProviderConstants.Gemini;

    public bool SupportsStreaming => true;

    public bool SupportsFunctionCalling => true;

    public bool SupportsVision => true;

    public bool SupportsEmbeddings => true;

    public async Task<ChatResponse> ChatAsync(
        ChatRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _chatService.ChatAsync(
            request,
            cancellationToken);
    }

    public async IAsyncEnumerable<string> StreamAsync(
        ChatRequest request,
        [System.Runtime.CompilerServices.EnumeratorCancellation]
        CancellationToken cancellationToken = default)
    {
        await foreach (var chunk in _chatService.StreamAsync(
            request,
            cancellationToken))
        {
            yield return chunk;
        }
    }

    public async Task<EmbeddingResponse> GenerateEmbeddingAsync(
        EmbeddingRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _embeddingService.GenerateEmbeddingAsync(
            request,
            cancellationToken);
    }

    public bool IsAvailable()
    {
        return true;
    }
}