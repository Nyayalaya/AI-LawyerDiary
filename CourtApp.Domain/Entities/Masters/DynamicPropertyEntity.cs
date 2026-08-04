using AuditTrail.Abstrations;
using CourtApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("entity_properties", Schema = "masters")]
    [Index(nameof(EntityType), nameof(EntityId), nameof(PropertyCode), IsUnique = true)]
    public class DynamicPropertyEntity : AuditableEntity
    {
        /// <summary>
        /// Module or Entity Name
        /// Example:
        /// MatterSubCategory
        /// MatterCategory
        /// MatterType
        /// Client
        /// Advocate
        /// Court
        /// Judge
        /// Organization
        /// </summary>
        public string EntityType { get; set; } = string.Empty;

        /// <summary>
        /// Id of the corresponding entity.
        /// </summary>
        public Guid EntityId { get; set; }

        public string PropertyCode { get; set; } = string.Empty;

        public string PropertyName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DynamicDataType DataType { get; set; }

        public DynamicControlType ControlType { get; set; }

        public bool IsRequired { get; set; }

        public bool IsVisible { get; set; } = true;

        public bool IsSearchable { get; set; }

        public bool IsReadOnly { get; set; }

        public bool AllowMultiple { get; set; }

        public int DisplayOrder { get; set; }

        public int? MaxLength { get; set; }

        public decimal? MinValue { get; set; }

        public decimal? MaxValue { get; set; }

        public string? DefaultValue { get; set; }

        public string? ValidationRegex { get; set; }

        /// <summary>
        /// Lookup Master Name
        /// Example:
        /// Country
        /// State
        /// CourtType
        /// Gender
        /// YesNo
        /// </summary>
        public string? LookupSource { get; set; }

        public string? Placeholder { get; set; }

        public string? HelpText { get; set; }

        public bool IsActive { get; set; } = true;
    }
}