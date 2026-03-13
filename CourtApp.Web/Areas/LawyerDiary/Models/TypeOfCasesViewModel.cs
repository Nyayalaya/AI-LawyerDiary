using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class TypeOfCasesViewModel
    {

        public Guid Id { get; set; }
        public SelectList CourtTypes { get; set; }
        public Guid CourtTypeId { get; set; }
        public string StateName { get; set; }
        public string CourtTypeName { get; set; }
        public SelectList CaseNatures { get; set; }
        public string CaseNature { get; set; }
        public Guid NatureId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public List<CaseType> CaseTypes { get; set; }
    }
    public class CaseType
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }

}
