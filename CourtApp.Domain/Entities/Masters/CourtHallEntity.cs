using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_court_hall")]
    [Index(nameof(Name), nameof(CourtComplexId), IsUnique = true)]
    public class CourtHallEntity : AuditableEntity
    {
        public required string Name { get; set; }

        public string Code { get; set; }

        public string JudgeName { get; set; }

        public string RoomNumber { get; set; }

        public Guid CourtComplexId { get; set; }
        public virtual CourtComplexEntity CourtComplex { get; set; }

        public Guid? CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }

        public bool IsActive { get; set; } = true;
        public int SeatingCapacity { get; set; }

        public bool HasVideoConference { get; set; }

        public List<LangEntity> Languages { get; set; }
            = new();
    }
}
