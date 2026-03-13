using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CaseKindViewModel
    {
        public SelectList CourtTypes { get; set; }
        public Guid Id { get; set; }
        public string CaseKind { get; set; }

        public Guid CourtTypeId { get; set; }
        public string CourtType { get; set; }
    }
}
