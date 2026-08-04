using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{

    [Table("m_case_stage", Schema = "masters")]
    [Index(nameof(Code), IsUnique = true)]
    public class CaseStageEntity : AuditableEntity
    {   
        public string Name { get; set; }
        public string Code { get; set; }
        public int Sequence { get; set; }
        public ICollection<CourtTypeEntity> CourtTypes { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}