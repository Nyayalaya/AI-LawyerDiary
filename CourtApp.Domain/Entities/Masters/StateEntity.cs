using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_state")]
    [Index(nameof(Code), IsUnique = true)]
    public class StateEntity:AuditableEntity
    {  

        public new int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<LangEntity> Languages { get; set; }
        public ICollection<DistrictEntity> Districts { get; set; }
    }
}