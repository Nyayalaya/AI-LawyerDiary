using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("m_book", Schema = "ad")]
    public class BookEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        public required string Name_En { get; set; }
        public string Name_Short { get; set; }
        public required int Year { get; set; }
        public required string Volume { get; set; }
        public required int SerialNo { get; set; }
        public DateTime? GazetteDate { get; set; }
        public int TypeId { get; set; }
        public string LegislativeNature { get; set; }
        public string TallyType { get; set; }
    }
}
