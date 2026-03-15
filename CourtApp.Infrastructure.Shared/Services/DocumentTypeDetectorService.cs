using CourtApp.Application.Features.CaseDocuments.Dtos;
using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Domain.Enums;
using System.IO;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class DocumentTypeDetectorService : IDocumentTypeDetectorService
    {
        public async Task<DocumentType> DetectAsync(DocumentStreamResult dsr)
        {
            var ext = Path.GetExtension(dsr.FileName).ToLower();

            switch (ext)
            {
                case ".pdf":
                    if (await IsScannedPdf(dsr.Stream))
                        return DocumentType.ScannedPdf;

                    return DocumentType.Pdf;

                case ".doc":
                case ".docx":
                    return DocumentType.Word;

                case ".jpg":
                case ".jpeg":
                case ".png":
                    return DocumentType.Image;

                default:
                    return DocumentType.Unknown;
            }
        }

        private async Task<bool> IsScannedPdf(Stream stream)
        {
            stream.Position = 0;

            using var reader = new StreamReader(stream, leaveOpen: true);
            var text = await reader.ReadToEndAsync();

            stream.Position = 0;

            return string.IsNullOrWhiteSpace(text);
        }
    }
}
