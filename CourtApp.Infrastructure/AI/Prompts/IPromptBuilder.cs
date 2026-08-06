using CourtApp.Infrastructure.AI.Models.Requests;
using Microsoft.SemanticKernel.ChatCompletion;

namespace CourtApp.Infrastructure.AI.Prompts;

public interface IPromptBuilder
{
    ChatHistory Build(ChatRequest request);
}