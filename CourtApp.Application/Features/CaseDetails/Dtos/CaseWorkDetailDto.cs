using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    public class CaseWorkDetailDto
    {
        public string WorkingDate { get; set; }
        public List<CaseWorkDto> Works { get; set; }
    }
}
