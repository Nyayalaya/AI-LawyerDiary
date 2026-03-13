using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_court_district", Schema = "ld")]
    public class CourtDistrictEntity : AuditableEntity
    {
              
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int StateId { get; set; }

        //[ForeignKey("District")]
        //public int DistrictCode { get; set; }        
        public string Abbreviation { get; set; }
        public virtual StateEntity State { get; set; }
        //public virtual DistrictEntity District { get; set; }        
    }
}
