using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.Cases
{
    public class ResponseGetAllCaseEntry
    {
        public Guid Id { get; set; }
        public string CaseTypeName { get; set; }
        public string CaseNumber { get; set; }
        public int CaseYear { get; set; }
        public string Title { get; set; }
        public string CourtType { get; set; }
        public string CourtName { get; set; }
        public string NextHearingDate { get; set; }
    }
}
