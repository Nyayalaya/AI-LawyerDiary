using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.Common
{
    [Table("m_state")]
    [Index(nameof(Code), IsUnique = true)]
    public class StateEntity
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<LangEntity> Languages { get; set; }
        public ICollection<DistrictEntity> Districts { get; set; }
    }
}