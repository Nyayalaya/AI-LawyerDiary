using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Domain.Enums;
using CourtApp.Worker.Models;
using CourtApp.Worker.Queues;
using Hangfire;

namespace CourtApp.Worker.Jobs
{
    public class TextExtractionJob
    {
        private readonly IPdfTextExtractorService _pdfService;
        private readonly IOcrService _ocrService;
        private readonly IWordDocumentExtractorService _wordService;

        public TextExtractionJob(
            IPdfTextExtractorService pdfService,
            IOcrService ocrService,
            IWordDocumentExtractorService wordService)
        {
            _pdfService = pdfService;
            _ocrService = ocrService;
            _wordService = wordService;
        }

        [Queue(QueueNames.Extraction)]
        [AutomaticRetry(Attempts = 3)]
        public async Task ExtractTextAsync(ProcessingContext context)
        {
            List<string> pagesTextData;

            switch (context.DocumentType)
            {
                case DocumentType.Pdf:
                    pagesTextData = await _pdfService.ExtractAsync(context.stream);
                    break;

                //case DocumentType.ScannedPdf:
                //    pagesTextData = await _ocrService.ExtractTextAsync(context.stream);
                //    break;

                //case DocumentType.Word:
                //    pagesTextData = await _wordService.ExtractAsync(context.DocumentId);
                //    break;

                default:
                    throw new Exception("Unsupported document type");
            }

            for (int i = 0; i < pagesTextData.Count; i++)
            {
                BackgroundJob.Enqueue<ChunkGenerationJob>(
                    x => x.GenerateChunk(context.DocumentId, i + 1, pagesTextData[i]));
            }
        }
    }
}
