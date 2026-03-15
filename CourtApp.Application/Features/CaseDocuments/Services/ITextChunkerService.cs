using CourtApp.Application.Features.CaseDocuments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Services
{
    public interface ITextChunkerService
    {
        List<TextChunkDto> GenerateChunk(Guid documentId,
                    string page,
                    int chunkSize = 1000,
                    int overlap = 200);
    }
}
