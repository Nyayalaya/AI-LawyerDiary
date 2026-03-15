using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Worker.Models;
using CourtApp.Worker.Queues;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CourtApp.Worker.Jobs
{
    public class DocumentPipelineJob
    {
        private readonly ILogger<DocumentPipelineJob> _logger;
        private readonly IDocumentStorageService _documentStorageService;
        private readonly IDocumentTypeDetectorService _documentTypeDetectorService;
        public DocumentPipelineJob(IDocumentStorageService documentStorageService,
            IDocumentTypeDetectorService documentTypeDetectorService, ILogger<DocumentPipelineJob> _logger)
        {
            this._documentStorageService = documentStorageService;
            this._documentTypeDetectorService = documentTypeDetectorService;
            this._logger = _logger;
        }

        [Queue(QueueNames.Documents)]
        [AutomaticRetry(Attempts = 3)]
        public async Task Start(Guid documentId, string documentPath)
        {
            try
            {
                _logger.LogInformation("Starting document pipeline for DocumentId: {DocumentId}", documentPath);
                var documentStreamResult = await _documentStorageService.DownloadAsync(documentPath);

                if (documentStreamResult == null)
                {
                    _logger.LogError("Document stream is null for DocumentId: {DocumentId}", documentId);
                    return;
                }

                var type = await _documentTypeDetectorService.DetectAsync(documentStreamResult);

                _logger.LogInformation("Detected document type {Type} for document id {documentId}", type, documentId);

                _logger.LogInformation("Enqueuing TextExtractionJob for {documentId}", documentId);

                var context = new ProcessingContext
                {
                    DocumentType = type,
                    DocumentId = documentId,
                    stream = documentStreamResult.Stream,
                    FilePath = documentPath
                };

                BackgroundJob.Enqueue<TextExtractionJob>(x => x.ExtractTextAsync(context));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing document pipeline for {DocumentId}", documentId);
                throw;
            }
        }
    }
}
