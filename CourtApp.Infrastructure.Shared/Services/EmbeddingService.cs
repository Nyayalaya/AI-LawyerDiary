using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Application.Interfaces.Contexts;
using CourtApp.Domain.Entities.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.Embeddings;
using Pgvector;
using System;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class EmbeddingService : IEmbeddingService
    {
        private readonly EmbeddingClient _embeddingClient;
        private readonly ILogger<EmbeddingService> _logger;
        private readonly IApplicationDbContext _dbContext;

        public EmbeddingService(IConfiguration config, 
            ILogger<EmbeddingService> logger,
            IApplicationDbContext _dbContext)
        {
            var apiKey = config["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("OpenAI API key is missing.");

            _embeddingClient = new EmbeddingClient("text-embedding-3-small", apiKey);
            _logger = logger;
            this._dbContext = _dbContext;
        }

        public async Task<Vector> GenerateEmbeddingAsync(
            Guid documentId,
            int pageNumber,
            string chunkText,Guid chunkId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(chunkText))
                {
                    _logger.LogWarning(
                        "Empty text for Document {DocumentId} Page {PageNumber}",
                        documentId,
                        pageNumber);

                    return new Vector(Array.Empty<float>());
                }

                const int maxLength = 8000;
                if (chunkText.Length > maxLength)
                {
                    chunkText = chunkText.Substring(0, maxLength);
                }

                var response = await _embeddingClient.GenerateEmbeddingAsync(chunkText);

                if (response?.Value == null)
                    throw new Exception("Embedding generation failed.");

                var embeddingMemory = response.Value.ToFloats();

                if (embeddingMemory.Span.Length == 0)
                    throw new Exception("Empty embedding returned.");

                float[] vectorArray = embeddingMemory.ToArray();

                var vector= new Vector(vectorArray);

                var embeddingEntity = new DocumentChunkEmbedding
                {
                    Id = Guid.NewGuid(),
                    ChunkId = chunkId,
                    Embedding = vector
                };

                await _dbContext.ChunkEmbeddings.AddAsync(embeddingEntity);
                await _dbContext.SaveChangesAsync(default);
                return new Vector(vectorArray);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Embedding generation failed for Document {DocumentId} Page {PageNumber}",
                    documentId,
                    pageNumber);

                throw;
            }
        }
    }
}
