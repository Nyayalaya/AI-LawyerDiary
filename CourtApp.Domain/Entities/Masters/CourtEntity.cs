using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_court", Schema = "masters")]
    [Index(nameof(Code), IsUnique = true)]
    public class CourtEntity : AuditableEntity
    {
        public required string Name { get; set; }

        public string Code { get; set; }

        public Guid CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }

        public int StateId { get; set; }
        public virtual StateEntity State { get; set; }

        public Guid? CourtDistrictId { get; set; }

        public virtual CourtDistrictEntity CourtDistrict { get; set; }

        public bool IsVirtualCourt { get; set; }

        public List<LangEntity> Languages { get; set; }
            = new();

        public ICollection<CourtComplexEntity> Complexes { get; set; }
            = new List<CourtComplexEntity>();
    }

}
