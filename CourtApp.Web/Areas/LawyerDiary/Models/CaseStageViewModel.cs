using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class CaseStageViewModel
    {
        public Guid Id { get; set; }
        public string CaseStage { get; set; }
    }
}
