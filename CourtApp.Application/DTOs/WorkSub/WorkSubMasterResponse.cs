using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.WorkSub
{
    public class WorkSubMasterResponse
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public Guid CourtTypeId { get; set; }
        public string WorkName { get; set; }
        public string CourtType { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
}
