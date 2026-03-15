using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Dtos
{
    public class TextChunkDto
    {
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }

        public int PageNumber { get; set; }

        public int ChunkIndex { get; set; }

        public string Text { get; set; }
    }
}
