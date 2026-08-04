using CourtApp.Infrastructure.AI.Models.DTOs;
using CourtApp.Infrastructure.AI.Models.Requests;
using Microsoft.SemanticKernel.ChatCompletion;

namespace CourtApp.Infrastructure.AI.Prompts;

public sealed class PromptBuilder
    : IPromptBuilder
{
    private readonly ISystemPromptProvider _systemPromptProvider;

    public PromptBuilder(
        ISystemPromptProvider systemPromptProvider)
    {
        _systemPromptProvider = systemPromptProvider;
    }

    public ChatHistory Build(ChatRequest request)
    {
        var history = new ChatHistory();

        history.AddSystemMessage(
            _systemPromptProvider.GetSystemPrompt(request));

        AddConversation(history, request);

        AddCurrentPrompt(history, request);

        return history;
    }

    private static void AddConversation(
        ChatHistory history,
        ChatRequest request)
    {
        if (request.Messages == null)
            return;

        foreach (var message in request.Messages)
        {
            AddMessage(history, message);
        }
    }

    private static void AddCurrentPrompt(
        ChatHistory history,
        ChatRequest request)
    {
        history.AddUserMessage(request.Prompt);
    }

    private static void AddMessage(
        ChatHistory history,
        ChatMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.Content))
            return;

        switch (message.Role?.Trim().ToLowerInvariant())
        {
            case "system":
                history.AddSystemMessage(message.Content);
                break;

            case "assistant":
                history.AddAssistantMessage(message.Content);
                break;

            default:
                history.AddUserMessage(message.Content);
                break;
        }
    }
}