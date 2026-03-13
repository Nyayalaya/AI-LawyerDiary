using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CourtDistrict
{
    public class CourtDistrictByIdReponse
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public int StateId { get; set; }
        //public int DistrictId { get; set; }
        //public int DistrictCode { get; set; }
    }
}
