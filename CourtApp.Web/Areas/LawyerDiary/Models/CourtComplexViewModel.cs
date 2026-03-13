using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CourtComplexViewModel
    {
        public string Message { get; set; }
        public SelectList States { get; set; }
        public SelectList CourtDistricts { get; set; }
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string CDistrictName { get; set; }
        //public string Abbreviation { get; set; } = null;
        public int StateId { get; set; }
        public Guid CourtDistrictId { get; set; }
        public List<Complex> Complexes { get; set; }
    }
    public class Complex
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
