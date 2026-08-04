using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.Masters
{
    [Table("matter_categories", Schema = "masters")]
    [Index(nameof(MatterTypeId), nameof(Code), IsUnique = true)]
    public class MatterCategoryEntity:AuditableEntity
    {
        public Guid MatterTypeId { get; set; }
        [ForeignKey(nameof(MatterTypeId))]
        public MatterTypeEntity MatterType { get; set; } = null!;
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<MatterSubCategoryEntity> SubCategories { get; set; }
            = new List<MatterSubCategoryEntity>();
    }
}
