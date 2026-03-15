using CourtApp.Application.Features.CaseDocuments.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tesseract;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class OcrService : IOcrService
    {
        public async Task<List<string>> ExtractTextAsync(Stream pageStream)
        {
        //    if (pageStream==null) return null;

        //    using var engine = new TesseractEngine("./tessdata", "eng");
        //    using var img = Pix.LoadFromFile(imagePath);
        //    using var page = engine.Process(img);

        //    return page.GetText();
        return null;
        }
    }
}
