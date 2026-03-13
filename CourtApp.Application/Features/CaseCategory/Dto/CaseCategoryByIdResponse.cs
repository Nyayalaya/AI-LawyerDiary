using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Dto
{
    public class CaseCategoryByIdResponse
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int StateId { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
