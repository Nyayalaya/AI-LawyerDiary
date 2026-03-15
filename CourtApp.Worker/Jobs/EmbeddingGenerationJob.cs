using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Worker.Queues;
using Hangfire;

namespace CourtApp.Worker.Jobs
{
    public class EmbeddingGenerationJob
    {
        private readonly IEmbeddingService _embeddingService;

        public EmbeddingGenerationJob(IEmbeddingService embeddingService)
        {
            _embeddingService = embeddingService;
        }

        [Queue(QueueNames.Embedding)]
        [AutomaticRetry(Attempts = 5)]
        public async Task GenerateEmbedding(Guid documentId, int pageNumber, string chunk,Guid chunkId)
        {
            await _embeddingService.GenerateEmbeddingAsync(documentId,pageNumber, chunk,chunkId);

            BackgroundJob.Enqueue<CitationExtractionJob>(
                x => x.ExtractAsync(documentId, chunk,pageNumber));
        }
    }
}
