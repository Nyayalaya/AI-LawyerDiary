using CourtApp.Application.Features.CaseDocuments.Services;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class WordDocumentExtractorService : IWordDocumentExtractorService
    {
        public async Task<List<string>> ExtractPagesAsync(Stream stream)
        {
            var pages = new List<string>();

            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var wordDoc = WordprocessingDocument.Open(memoryStream, false);

            var body = wordDoc.MainDocumentPart.Document.Body;

            var pageText = "";

            foreach (var element in body.Elements())
            {
                if (element is Paragraph paragraph)
                {
                    pageText += paragraph.InnerText + Environment.NewLine;
                }

                if (element is Break breakElement && breakElement.Type == BreakValues.Page)
                {
                    pages.Add(pageText);
                    pageText = "";
                }
            }

            if (!string.IsNullOrWhiteSpace(pageText))
                pages.Add(pageText);

            return pages;
        }
    }
}
