using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.ProcSubHead
{
    public class GetProcSubHeadByIdResponse
    {
        public Guid Id { get; set; }
        public Guid HeadId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
