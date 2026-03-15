using CourtApp.Application.Features.CaseDocuments.Dtos;
using CourtApp.Application.Features.CaseDocuments.Expressions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CourtApp.Application.Features.CaseDocuments.Parsers
{
    public static class LegalCitationParser
    {
        public static List<LegalCitationDto> Parse(string text, int pageNumber)
        {
            var results = new List<LegalCitationDto>();

            foreach (Match match in LegalCitationRegex.SectionRegex.Matches(text))
            {
                results.Add(new LegalCitationDto
                {
                    Section = match.Groups[1].Value,
                    ActName = match.Groups[2].Value,
                    CitationContent = match.Value,
                    CitationType = "Section",
                    PageNumber = pageNumber
                });
            }

            foreach (Match match in LegalCitationRegex.AirRegex.Matches(text))
            {
                results.Add(new LegalCitationDto
                {
                    CitationContent = match.Value,
                    CitationType = "AIR",
                    PageNumber = pageNumber
                });
            }

            foreach (Match match in LegalCitationRegex.SccRegex.Matches(text))
            {
                results.Add(new LegalCitationDto
                {
                    CitationContent = match.Value,
                    CitationType = "SCC",
                    PageNumber = pageNumber
                });
            }

            return results;
        }
    }
}
