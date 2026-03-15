using CourtApp.Application.Features.CaseDocuments.Dtos;
using CourtApp.Application.Features.CaseDocuments.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class DocumentStorageService : IDocumentStorageService
    {   
        public async Task<DocumentStreamResult> DownloadAsync(string documentPath)
        {
            if (!File.Exists(documentPath))
                throw new FileNotFoundException($"Document not found {documentPath}");

            var stream = new FileStream(documentPath, FileMode.Open, FileAccess.Read);

            var extension = Path.GetExtension(documentPath);

            return new DocumentStreamResult
            {
                Stream = stream,
                FileName = Path.GetFileName(documentPath),
                ContentType = extension
            };
        }
    }
}
