using System;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Services
{
    public interface ILegalCitationExtractorService
    {
        Task ExtractAndStoreAsync(Guid chunkId, string text, int pageNumber);
    }
}
