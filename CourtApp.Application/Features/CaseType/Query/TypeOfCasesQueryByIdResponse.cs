using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Typeofcasess.Query
{
    public class TypeOfCasesQueryByIdResponse
    {
        public Guid Id { get; set; }
        public Guid NatureId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }        
        public Guid CourtTypeId { get; set; }
        public int StateId { get; set; }
    }
}
