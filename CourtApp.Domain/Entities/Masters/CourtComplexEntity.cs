using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_court_complex")]
    [Index(nameof(Name), nameof(StateId), IsUnique = true)]
    public class CourtComplexEntity : AuditableEntity
    {
        public required string Name { get; set; }

        public string Code { get; set; }

        public Guid CourtId { get; set; }
        public virtual CourtEntity Court { get; set; }

        // Administrative district
        public int StateId { get; set; }
        public virtual StateEntity State { get; set; }

        // Judiciary district
        public Guid? CourtDistrictId { get; set; }
        public virtual CourtDistrictEntity CourtDistrict { get; set; }

        public bool IsVirtualComplex { get; set; }
        public string Address { get; set; }

        public List<LangEntity> Languages { get; set; }
            = new();

        public ICollection<CourtHallEntity> CourtHalls { get; set; }
            = new List<CourtHallEntity>();
    }
}
