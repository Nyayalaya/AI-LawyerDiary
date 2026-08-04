using CourtApp.Infrastructure.AI.Models.Requests;
using CourtApp.Infrastructure.AI.Models.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Services;

public interface ILawyerAssistant
{
    Task<ChatResponse> ChatAsync(
        ChatRequest request,
        CancellationToken cancellationToken = default);
}