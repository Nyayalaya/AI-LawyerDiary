using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster
{
    public class GetWorkMasterResponse
    {
        public Guid Id { get; set; }
        public string Work_En { get; set; }
        public string Work_Hn { get; set; }
        public string Abbreviation { get; set; }

    }
}
