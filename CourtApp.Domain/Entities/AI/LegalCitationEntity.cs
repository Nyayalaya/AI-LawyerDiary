using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.AI
{
    [Table("chunk_citation", Schema = "ai")]
    public class LegalCitationEntity : AuditableEntity
    {
        public Guid ChunkId { get; set; }

        public string ActName { get; set; }

        public string Section { get; set; }

        public string CitationContent { get; set; }

        public string CitationType { get; set; }

        public int? PageNumber { get; set; }

        public string NormalizedCitation { get; set; }

        public DocumentChunk Chunk { get; set; }
    }
}
