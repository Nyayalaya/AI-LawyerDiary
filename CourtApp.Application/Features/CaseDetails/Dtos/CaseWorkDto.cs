using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    public class CaseWorkDto
    {
        public string WorkType { get; set; }
        public string Work { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string AppliedOn { get; set; }
    }
}
