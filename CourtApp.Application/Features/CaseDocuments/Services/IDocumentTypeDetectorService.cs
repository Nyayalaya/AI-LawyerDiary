using CourtApp.Application.Features.CaseDocuments.Dtos;
using CourtApp.Domain.Enums;
using System.IO;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Services
{
    public interface IDocumentTypeDetectorService
    {
        Task<DocumentType> DetectAsync(DocumentStreamResult documentStreamResult);
    }
}
