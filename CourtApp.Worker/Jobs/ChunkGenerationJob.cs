using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Worker.Queues;
using Hangfire;

namespace CourtApp.Worker.Jobs
{
    public class ChunkGenerationJob
    {
        private readonly ITextChunkerService _chunkService;

        public ChunkGenerationJob(ITextChunkerService chunkService)
        {
            _chunkService = chunkService;
        }

        [Queue(QueueNames.Chunking)]
        public async Task GenerateChunk(Guid documentId, int pageNumber, string text)
        {
            var chunks = _chunkService.GenerateChunk(documentId, text);

            foreach (var chunkData in chunks)
            {
                BackgroundJob.Enqueue<EmbeddingGenerationJob>(
                    x => x.GenerateEmbedding(documentId, pageNumber, chunkData.Text,chunkData.Id));
            }
        }
    }
}
