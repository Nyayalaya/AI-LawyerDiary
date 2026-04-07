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
        public int StateId { get; set; }
        public virtual StateEntity State { get; set; }

        public Guid CourtDistrictId { get; set; }
        public virtual CourtDistrictEntity CourtDistrict { get; set; }

        public Guid? CourtId { get; set; }
        public virtual CourtEntity Court { get; set; }

        public Guid? LocationId { get; set; }
        public virtual LocationEntity Location { get; set; }

        public List<LangEntity> Languages { get; set; }
        public ICollection<CourtHallEntity> CourtHalls { get; set; }
    }
}
