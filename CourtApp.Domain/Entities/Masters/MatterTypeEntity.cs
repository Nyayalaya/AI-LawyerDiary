using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.Masters
{
    [Table("matter_types", Schema = "masters")]
    [Index(nameof(Code), IsUnique = true)]
    public class MatterTypeEntity : AuditableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<MatterCategoryEntity> Categories { get; set; }
            = new List<MatterCategoryEntity>();
    }
}
