using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("r_case_docs", Schema = "ld")]
    public class CaseDocsEntity:AuditableEntity
    {
        
        public Guid CaseId { get; set; }
        public int DOTypeId { get; set; }
        public Guid DOId { get; set; }
        public string Path { get; set; }
        public DateTime DocDate { get; set; }
        public virtual DOTypeEntity DO { get; set; }
    }
}
