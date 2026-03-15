using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Services
{
    public interface IWordDocumentExtractorService
    {
        Task<List<string>> ExtractPagesAsync(Stream stream);
    }
}
