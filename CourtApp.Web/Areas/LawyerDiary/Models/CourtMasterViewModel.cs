using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CourtMasterViewModel
    {        
        public SelectList CourtTypes { get; set; }
        public SelectList States { get; set; }
        //public SelectList Districts { get; set; }
        public SelectList CourtDistricts { get; set; }
        public SelectList CourtComplexes{ get; set; }
        //public int DistrictCode { get; set; }
        public int  StateCode { get; set; }
        public Guid CourtTypeId { get; set; }
        public string CourtType { get; set; }
        //public string CourtName { get; set; }       
        public Guid Id { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? CourtComplexId { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Complex { get; set; }
        public string Total { get; set; }
        public bool IsHighCourt { get; set; }
        public List<CourtBench> CourtBenches { get; set; }
}

    public class CourtBench
    {
        public string CourtBench_En { get; set; }
        //public string CourtBench_Hn { get; set; }
        public string Address { get; set; }
    }
}
