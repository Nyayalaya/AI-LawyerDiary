using Pgvector;
using System;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Services
{
    public interface IEmbeddingService
    {
        Task<Vector> GenerateEmbeddingAsync(Guid documentId,int pageNumber,string text, Guid chunckId);
    }
}
