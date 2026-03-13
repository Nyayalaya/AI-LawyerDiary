using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("m_act_book",Schema ="ad")]
    public class ActBookEntity: AuditableEntity
    {   
        public int BookId { get; set; }
        public int BookYear { get; set; }
        public string BookPageNo { get; set; }
        public int? BookSrNo { get; set; }
        public string Volume { get; set; }
        
        [ForeignKey("ActId")]
        public ActEntity Act { get; set; }
    }
}
