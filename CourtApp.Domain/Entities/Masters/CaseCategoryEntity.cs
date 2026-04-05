using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_case_category")]
    [Index(nameof(Code), IsUnique = true)]
    public class CaseCategoryEntity : AuditableEntity
    {               
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}