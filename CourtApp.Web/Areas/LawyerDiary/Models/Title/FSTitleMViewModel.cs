using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models.Title
{
    public class FSTitleMViewModel
    {
        public Guid Id { get; set; }
        public SelectList FSTypes { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
