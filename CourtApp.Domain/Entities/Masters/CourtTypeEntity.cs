using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_court_type")]
    [Index(nameof(Code), IsUnique = true)]
    public class CourtTypeEntity : AuditableEntity
    {        
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid CourtLevelId { get; set; }
        public CourtLevelEntity CourtLevel { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}