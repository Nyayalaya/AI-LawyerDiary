using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.AI;
using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("case_documents",Schema ="cases")]
    public class CaseDocsEntity:AuditableEntity
    {   
        public Guid CaseId { get; set; }
        public int DOTypeId { get; set; }
        public Guid DOId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public DateTime DocDate { get; set; }
        public virtual DOTypeEntity DO { get; set; }
        public long FileSize { get; set; }
        public bool IsProcessed { get; set; }
        public CaseDetailEntity Case { get; set; }
        public ICollection<DocumentChunk> Chunks { get; set; }
    }
}
