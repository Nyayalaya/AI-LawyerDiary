using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
    namespace CourtApp.Domain.Entities.Masters
    {
    [Table("matter_subcategories", Schema = "masters")]
    [Index(nameof(MatterCategoryId), nameof(Code), IsUnique = true)]
    public class MatterSubCategoryEntity : AuditableEntity
    {
        public Guid MatterCategoryId { get; set; }

        [ForeignKey(nameof(MatterCategoryId))]
        public MatterCategoryEntity MatterCategory { get; set; } = null!;

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        
    }
}
