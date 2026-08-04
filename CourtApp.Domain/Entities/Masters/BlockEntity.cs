using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_block",Schema ="common")]   
    public class BlockEntity
    {        
        public required int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int DistrictId { get; set; }
        public virtual DistrictEntity District { get; set; }
    }
}
