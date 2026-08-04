using CourtApp.Infrastructure.AI.Kernels.Manager;
using CourtApp.Infrastructure.AI.Models.Requests;
using CourtApp.Infrastructure.AI.Models.Responses;
using Microsoft.SemanticKernel.Embeddings;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Providers.Gemini;


public sealed class GeminiEmbeddingService
{

    private readonly IKernelManager _kernelManager;


    public GeminiEmbeddingService(
        IKernelManager kernelManager)
    {
        _kernelManager = kernelManager;
    }



    public async Task<EmbeddingResponse> GenerateEmbeddingAsync(
        EmbeddingRequest request,
        CancellationToken cancellationToken = default)
    {

        var kernel =
            _kernelManager.GetKernel();


        var embeddingService =
            kernel.GetRequiredService<
                ITextEmbeddingGenerationService>();



        var vector =
            await embeddingService.GenerateEmbeddingAsync(
                request.Text,
                kernel,
                cancellationToken);



        return new EmbeddingResponse
        {
            Embedding = vector.ToArray(),

            Model = "text-embedding-004",

            Provider = "Gemini"
        };

    }

}