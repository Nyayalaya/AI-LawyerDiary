using CourtApp.Infrastructure.AI.Models.Requests;
namespace CourtApp.Infrastructure.AI.Prompts
{
    public interface ISystemPromptProvider
    {
        string GetSystemPrompt(ChatRequest request);
    }
}
