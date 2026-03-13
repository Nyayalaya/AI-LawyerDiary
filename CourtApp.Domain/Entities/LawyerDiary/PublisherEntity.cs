using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_publisher",Schema ="ld")]
    public class PublisherEntity : AuditableEntity
    {
        
        [Required]
        public string PublicationName { get; set; }        
        public string PropriatorName { get; set; }
        public ICollection<LDBookEntity> lDBookEntities { get; set; }
    }
}