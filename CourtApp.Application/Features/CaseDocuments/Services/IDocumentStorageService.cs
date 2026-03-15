using CourtApp.Application.Features.CaseDocuments.Dtos;
using System;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Services
{
    public interface IDocumentStorageService
    {
        Task<DocumentStreamResult> DownloadAsync(string documentPath);
    }
}
