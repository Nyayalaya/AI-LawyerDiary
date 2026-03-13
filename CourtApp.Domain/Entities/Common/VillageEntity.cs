using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using AuditTrail.Abstrations;
using System.Collections.Generic;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_village")]
    public class VillageEntity
    {
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int GpId { get; set; }
        public virtual GPEntity Gp { get; set; }
        public ICollection<HabitationEntity> Havitations { get; set; }
    }
}
