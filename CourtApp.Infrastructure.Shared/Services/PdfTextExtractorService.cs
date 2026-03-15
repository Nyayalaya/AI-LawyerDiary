using CourtApp.Application.Features.CaseDocuments.Services;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class PdfTextExtractorService : IPdfTextExtractorService
    {  
        public async Task<List<string>> ExtractAsync(Stream stream)
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var pagesTextData = new List<string>();

            using (var document = PdfDocument.Load(memoryStream))
            {
                for (int i = 0; i < document.PageCount; i++)
                {
                    pagesTextData.Add(document.GetPdfText(i));
                }
            }

            return pagesTextData;
        }
    }
}
