using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_court", Schema = "ld")]
    public class CourtMasterEntity : AuditableEntity
    {
        public int StateId { get; set; }
        public Guid CourtTypeId { get; set; }
        //public int DistrictId { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? CourtComplexId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public virtual StateEntity State { get; set; }
        //public virtual DistrictEntity District { get; set; }
        public virtual CourtComplexEntity CourtComplex { get; set; }
        public virtual CourtDistrictEntity CourtDistrict { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
        public virtual ICollection<CourtBenchEntity> CourtBenches { get; set; } = new List<CourtBenchEntity>();
    }
}