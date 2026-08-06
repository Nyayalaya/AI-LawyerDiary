using CourtApp.Infrastructure.AI.Configuration.Core;
using CourtApp.Infrastructure.AI.Kernels.Manager;
using CourtApp.Infrastructure.AI.Models.Requests;
using CourtApp.Infrastructure.AI.Models.Responses;
using CourtApp.Infrastructure.AI.Prompts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace CourtApp.Infrastructure.AI.Providers.Gemini;


public sealed class GeminiChatService
{
    private readonly IKernelManager _kernelManager;
    private readonly IPromptBuilder _promptBuilder;
    private readonly AIOptions _options;
    private readonly ILogger<GeminiChatService> _logger;


    public GeminiChatService(IKernelManager kernelManager,
        IPromptBuilder promptBuilder,
        IOptions<AIOptions> options,
        ILogger<GeminiChatService> logger)
    {
        _kernelManager = kernelManager;
        _promptBuilder = promptBuilder;
        _options = options.Value;
        _logger = logger;
    }



    public async Task<ChatResponse> ChatAsync(ChatRequest request,CancellationToken cancellationToken = default)
    {

        var kernel = _kernelManager.GetKernel();
        var chatService = kernel.GetRequiredService<IChatCompletionService>();
        var history =  _promptBuilder.Build(request);
        var settings = CreateExecutionSettings(request);
        _logger.LogInformation( "Sending request to Gemini. Model:{Model}", _options.Gemini.Model);
        
        var result = await chatService.GetChatMessageContentAsync(
                history,
                settings,
                kernel,
                cancellationToken);



        return new ChatResponse
        {
            Success = true,

            Content =
                result.Content ?? string.Empty,


            Provider =
                _options.Provider.CurrentProvider,


            Model =
                _options.Gemini.Model,


            History =
                request.Messages
        };

    }



    private GeminiPromptExecutionSettings CreateExecutionSettings(ChatRequest request)
    {

        return new GeminiPromptExecutionSettings
        {

            Temperature =(float)_options.Gemini.Temperature,
            TopP =(float)_options.Gemini.TopP,
            TopK = _options.Gemini.TopK,
            CandidateCount =_options.Gemini.CandidateCount,
            MaxTokens =_options.General.DefaultMaxTokens,
            FunctionChoiceBehavior =FunctionChoiceBehavior.Auto(),
            ModelId = _options.Gemini.Model

        };

    }

    public async IAsyncEnumerable<string> StreamAsync(
    ChatRequest request,
    [System.Runtime.CompilerServices.EnumeratorCancellation]
    CancellationToken cancellationToken = default)
    {

        var kernel =
            _kernelManager.GetKernel();


        var chatService =
            kernel.GetRequiredService<IChatCompletionService>();


        var history =
            _promptBuilder.Build(request);


        var settings =
            CreateExecutionSettings(request);



        await foreach (
            var chunk
            in chatService.GetStreamingChatMessageContentsAsync(
                history,
                settings,
                kernel,
                cancellationToken))
        {

            if (!string.IsNullOrEmpty(chunk.Content))
            {
                yield return chunk.Content;
            }

        }

    }

}
