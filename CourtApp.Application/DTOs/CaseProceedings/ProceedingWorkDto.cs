using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseProceedings
{
    public class ProceedingWorkDto
    {
        public List<PrWork> Workdt { get; set; }
        public DateTime? WorkingDate { get; set; }
    }
    public class PrWork
    {
        public Guid WorkTypeId { get; set; }
        public Guid WorkId { get; set; }
    }
}
