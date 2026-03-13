using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CaseNatureViewModel
    {
        public Guid Id { get; set; }
        public SelectList CourtTypes { get; set; }
        //public SelectList States { get; set; }
        public Guid CourtTypeId { get; set; }
        //public int StateId { get; set; }
        public string StateName { get; set; }
        public string CourtType { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
