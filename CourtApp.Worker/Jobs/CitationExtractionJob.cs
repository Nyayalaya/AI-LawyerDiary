using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Worker.Queues;
using Hangfire;

namespace CourtApp.Worker.Jobs
{
    public class CitationExtractionJob
    {
        private readonly ILegalCitationExtractorService _citationService;

        public CitationExtractionJob(
            ILegalCitationExtractorService citationService)
        {
            _citationService = citationService;
        }

        [Queue(QueueNames.Citation)]
        [AutomaticRetry(Attempts = 3)]
        [JobDisplayName("Extract Legal Citations from Chunk")]
        public async Task ExtractAsync(Guid chunkId, string text, int pageNumber)
        {
            if (chunkId == Guid.Empty || string.IsNullOrWhiteSpace(text))
                return;

            await _citationService.ExtractAndStoreAsync(
                chunkId,
                text,
                pageNumber);
        }
    }
}
