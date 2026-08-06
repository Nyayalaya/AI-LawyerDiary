using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Dto
{
    public class CaseCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CourtType { get; set; }        
    }
}
