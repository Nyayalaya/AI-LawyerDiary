using AuditTrail.Abstrations;
using Pgvector;
using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace CourtApp.Domain.Entities.AI
{
    [Table("document_embedding", Schema = "ai")]
    public class DocumentChunkEmbedding:AuditableEntity
    {
        public Guid ChunkId { get; set; }

        public Vector Embedding { get; set; }

        public DocumentChunk Chunk { get; set; }
    }
}
