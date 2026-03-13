using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_nature", Schema = "ld")]
    public class NatureEntity : AuditableEntity
    {               
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public Guid CourtTypeId { get; set; }        
        public string Abbreviation { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }        
    }
}