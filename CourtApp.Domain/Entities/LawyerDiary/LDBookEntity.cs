using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_book", Schema = "ld")]
    public class LDBookEntity : AuditableEntity
    {
        public Guid BookTypeId { get; set; }
        public virtual BookTypeEntity BookType { get; set; }
        public Guid PublisherId { get; set; }
        public virtual PublisherEntity Publisher { get; set; }
        public int Year { get; set; }
    }
}
