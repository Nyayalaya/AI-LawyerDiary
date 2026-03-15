using CourtApp.Application.Features.CaseDocuments.Parsers;
using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Application.Interfaces.Contexts;
using CourtApp.Domain.Entities.AI;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class LegalCitationExtractorService : ILegalCitationExtractorService
    {
        private readonly IApplicationDbContext _context;

        public LegalCitationExtractorService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task ExtractAndStoreAsync(Guid chunkId, string text, int pageNumber)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            var citations = LegalCitationParser.Parse(text, pageNumber);

            if (!citations.Any())
                return;

            var entities = citations.Select(c => new LegalCitationEntity
            {
                Id = Guid.NewGuid(),
                ChunkId = chunkId,
                ActName = c.ActName,
                Section = c.Section,
                CitationContent = c.CitationContent,
                CitationType = c.CitationType,
                PageNumber = pageNumber,
                NormalizedCitation = $"{c.Section} {c.ActName}".Trim()
            }).ToList();

            entities = entities
                    .GroupBy(x => x.NormalizedCitation)
                    .Select(x => x.First())
                    .ToList();

            await _context.LegalCitations.AddRangeAsync(entities);

            await _context.SaveChangesAsync(CancellationToken.None);

        }
    }

}
