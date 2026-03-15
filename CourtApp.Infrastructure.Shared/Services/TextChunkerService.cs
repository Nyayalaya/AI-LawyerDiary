using CourtApp.Application.Features.CaseDocuments.Dtos;
using CourtApp.Application.Features.CaseDocuments.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class TextChunkerService : ITextChunkerService
    {
        
        public List<TextChunkDto> GenerateChunk(Guid documentId,string page, int chunkSize = 1000, int overlap = 200)
        {
            if (string.IsNullOrWhiteSpace(page))
                return new List<TextChunkDto>();

            var chunks = new List<TextChunkDto>();
            int start = 0;
            int chunkIndex = 0;
            int pageNumber = 1; // optionally pass this as parameter

            while (start < page.Length)
            {
                int length = Math.Min(chunkSize, page.Length - start);
                string chunkText = page.Substring(start, length);

                var chunk = new TextChunkDto
                {
                    Id = Guid.NewGuid(),
                    DocumentId = documentId,
                    PageNumber = pageNumber,
                    ChunkIndex = chunkIndex,
                    Text = chunkText
                };

                chunks.Add(chunk);

                start += (chunkSize - overlap);
                chunkIndex++;
            }

            return chunks;
        }
    }
}
