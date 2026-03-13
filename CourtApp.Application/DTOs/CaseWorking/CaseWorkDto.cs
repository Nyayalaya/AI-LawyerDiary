using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseWorking
{
    public class CaseWorkDto
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public string CaseTitle { get; set; }
        public string Work { get; set; }
        public Guid WorkId { get; set; }
        public string SubWork { get; set; }
        public Guid SubWId { get; set; }
        public string WDate { get; set; }
        public int Status { get; set; }
    }    
}
