using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using AuditTrail.Abstrations;
using System.Collections.Generic;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_gp")]    
    public class GPEntity
    {
        
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int BlockId { get; set; }
        public virtual BlockEntity Block { get; set; }
        public ICollection<VillageEntity> Villages { get; set; }
    }
}
