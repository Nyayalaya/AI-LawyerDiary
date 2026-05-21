using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_court_level")]
    [Index(nameof(Code), IsUnique = true)]
    public class CourtLevelEntity:AuditableEntity
    {

        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<CourtTypeEntity> CourtTypes { get; set; }
            = new List<CourtTypeEntity>();
    }
}
