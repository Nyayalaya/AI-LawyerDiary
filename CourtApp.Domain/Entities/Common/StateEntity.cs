using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.Common
{
    [Table("m_state")]
    public class StateEntity
    {
        
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public ICollection<DistrictEntity> Districts { get; set; }
    }
}