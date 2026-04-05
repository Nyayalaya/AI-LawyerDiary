using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_district")]    
    public class DistrictEntity
    {
       
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int StateId { get; set; }
        public virtual StateEntity State { get; set; }
        public ICollection<BlockEntity> Blocks { get; set; }
        public ICollection<CityEntity> Cities { get; set; }
    }
}