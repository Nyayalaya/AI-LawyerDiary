using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_book_type", Schema = "ld")]
    public class BookTypeEntity : AuditableEntity
    {        
        
        public  string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public ICollection<LDBookEntity> lDBookEntities { get; set; }
    }
}