using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_court_hall")]
    [Index(nameof(Code), nameof(CourtComplexId), IsUnique = true)]
    public class CourtHallEntity : AuditableEntity
    {
        public required string Name { get; set; }
        public string Code { get; set; }
        public string JudgeName { get; set; }
        public string RoomNumber { get; set; }
        public Guid CourtComplexId { get; set; }
        public virtual CourtComplexEntity CourtComplex { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}
