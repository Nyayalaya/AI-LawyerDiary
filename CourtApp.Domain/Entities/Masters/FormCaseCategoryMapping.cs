using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_form_case_category_mapping", Schema = "masters")]
    [Index(nameof(FormSubtypeId), nameof(CaseCategoryId), IsUnique = true)]
    public class FormCaseCategoryMapping : AuditableEntity
    {
        public Guid FormSubtypeId { get; set; }

        public Guid CaseCategoryId { get; set; }

        public bool IsMandatory { get; set; } = false;

        public bool IsActive { get; set; } = true;
    }
}
