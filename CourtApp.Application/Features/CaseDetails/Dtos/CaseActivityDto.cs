using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    public class CaseActivityDto
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string NextDate { get; set; }
        public string Stage { get; set; }
        public string Activity { get; set; }
        public List<CaseWorkDetailDto> WorkDetail { get; set; }
    }
}
