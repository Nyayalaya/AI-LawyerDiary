using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_court_complex", Schema = "ld")]
    public class CourtComplexEntity : AuditableEntity
    {        
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int StateId { get; set; }
        //public int DistrictCode { get; set; }
        public Guid CourtDistrictId { get; set; }
        public string Abbreviation { get; set; }
        public virtual StateEntity State { get; set; }
        //public virtual DistrictEntity District { get; set; }
        public virtual CourtDistrictEntity CourtDistrict { get; set; }
    }
}
