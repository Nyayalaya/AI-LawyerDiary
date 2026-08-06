using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_judge", Schema = "masters")]
    public class JudgeEntity : AuditableEntity
    {
       
        public string Name { get; set; }
        public Guid CourtHallId { get; set; }
        public CourtHallEntity CourtHall { get; set; }
    }

}
