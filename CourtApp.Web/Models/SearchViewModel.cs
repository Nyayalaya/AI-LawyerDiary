using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtApp.Web.Models
{
    public class SearchViewModel
    {
        public SelectList CourtTypes { get; set; }
        public int CourtTypeCode { get; set; }
        public SelectList Courts { get; set; }
        public string CourtId { get; set; }
        public SelectList Typeofcasess { get; set; }
        public string TypeofcasesId { get; set; }
        public SelectList SearchTypes { get; set; }
        public string SearchBy { get; set; }
        public SelectList States { get; set; }
        public string StateCode { get; set; }
        public SelectList Districts { get; set; }
        public int DistrictCode { get; set; }
        public string CaseYear { get; set; }
        public string CaseNumber { get; set; }
        public SelectList Cases { get; set; }
        public string CaseId { get; set; }

    }
}