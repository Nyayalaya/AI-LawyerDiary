using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseDetails
{
    public class GetCaseSearchResponse
    {
        public Guid Id { get; set; }      
        public string NoYear { get; set; }
        public string Title { get; set; }
        public string DocFilePath { get; set; }

        public string Court { get; set; }
        public string CaseType { get; set; }
        public string No { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
    }
}
