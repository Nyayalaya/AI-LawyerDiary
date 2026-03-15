using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.LawyerDiary;
using Pgvector;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.AI
{
    [Table("document_chunk", Schema = "ai")]
    public class DocumentChunk:AuditableEntity
    {
        public Guid DocumentId { get; set; }

        public string Content { get; set; }

        public int ChunkIndex { get; set; }

        public int PageNumber { get; set; }

        public string ActName { get; set; }

        public string SectionName { get; set; }

        public string CourtName { get; set; }

        public string JudgeName { get; set; }

        public string Citation { get; set; }

        public int TokenCount { get; set; }

        public string Language { get; set; }

        public string SourceType { get; set; }

        public string MetadataJson { get; set; }

        public CaseDocsEntity Document { get; set; }

        public DocumentChunkEmbedding Embedding { get; set; }
    }
}
