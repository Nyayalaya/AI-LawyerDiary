using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_court")]
    [Index(nameof(Name), nameof(LocationId), IsUnique = true)]
    public class CourtEntity : AuditableEntity
    {
        public required string Name { get; set; }    // Rajasthan High Court

        public Guid CourtLevelId { get; set; }
        public virtual CourtLevelEntity CourtLevel { get; set; }

        public Guid CourtTypeId { get; set; }
        public CourtTypeEntity CourtType { get; set; }

        public Guid LocationId { get; set; }    // Jaipur Bench / District
        public LocationEntity Location { get; set; }

        public List<LangEntity> Languages { get; set; } = new();

        public ICollection<CourtComplexEntity> Complexes { get; set; }
    }

}
