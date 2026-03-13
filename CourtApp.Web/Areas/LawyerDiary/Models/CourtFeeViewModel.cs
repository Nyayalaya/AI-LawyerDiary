using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CourtFeeStructureViewModel
    {
        public Guid Id { get; set; }   
        public SelectList States { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public Double MinValue { get; set; }
        public Double MaxValue { get; set; }
        public Double Rate { get; set; }
        public Double FixAmount { get; set; }
    }
}
