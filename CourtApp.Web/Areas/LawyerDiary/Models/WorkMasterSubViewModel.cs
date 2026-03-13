using CourtApp.Domain.Entities.LawyerDiary;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class WorkMasterSubViewModel
    {
        public string Message { get; set; }
        public SelectList WMasters { get; set; }
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public string WorkName { get; set; }
        public  string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public List<WorkSubMaster> Works { get; set; }
    }
    public class WorkSubMaster
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        //public string Abbreviation { get; set; }
    }
}
