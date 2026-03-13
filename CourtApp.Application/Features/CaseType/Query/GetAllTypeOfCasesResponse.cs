using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Typeofcasess.Query
{
    public class GetAllTypeOfCasesResponse
    {
        public Guid Id { get; set; }
        public string CaseNature { get; set; }
        public string Name_En { get; set; }        
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public string StateName { get; set; }
        public string CourtTypeName { get; set; }
    }
}
