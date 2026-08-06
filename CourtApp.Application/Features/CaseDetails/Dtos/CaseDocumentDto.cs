using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    public class CaseDocumentDto
    {
        public Guid Id { get; set; }
        public string DocType { get; set; }
        public string DocName { get; set; }
        public string DocFilePath { get; set; }
        public string DocDate { get; set; }
    }
}
