using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CourtDistrictViewModel
    {
        public string Message { get; set; }
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public int StateId { get; set; }
        public SelectList States { get; set; }
        public List<CourtDistrict> CourtDistricts { get; set; }
    }
    public class CourtDistrict
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
