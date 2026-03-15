using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Dtos
{
    public class LegalCitationDto
    {
        public string ActName { get; set; }

        public string Section { get; set; }

        public string CitationContent { get; set; }

        public string CitationType { get; set; }

        public int PageNumber { get; set; }
    }
}
