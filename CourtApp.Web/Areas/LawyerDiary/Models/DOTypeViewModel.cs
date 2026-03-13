using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class DOTypeViewModel
    {
        public Guid Id { get; set; }
        public SelectList Types{ get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string Name_En { get; set; }
    }
}
