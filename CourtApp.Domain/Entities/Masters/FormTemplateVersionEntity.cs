using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_form_template_version", Schema = "masters")]
    public class FormTemplateVersionEntity : AuditableEntity
    {
        public Guid FormTemplateId { get; set; }

        public string Content { get; set; }

        public string Version { get; set; }

        public bool IsPublished { get; set; }

        public virtual FormTemplateEntity FormTemplate { get; set; }
    }
}
