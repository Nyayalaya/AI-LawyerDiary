using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseDetails
{
    public class CaseProceedingResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Head { get; set; }
        public string SubHead { get; set; }
        public string NextStage { get; set; }
        public string NextDate { get; set; }
    }
}
