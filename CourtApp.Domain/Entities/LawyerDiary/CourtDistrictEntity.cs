using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_court_district")]
    [Index(nameof(Name), nameof(StateId), IsUnique = true)]
    public class CourtDistrictEntity : AuditableEntity
    {         
        public required string Name { get; set; }
        public int StateId { get; set; }
        public List<LangEntity> Languages { get; set; }
        public virtual StateEntity State { get; set; }
    }
}
