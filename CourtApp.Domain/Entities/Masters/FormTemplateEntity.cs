using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_form_template", Schema = "masters")]
    public class FormTemplateEntity : AuditableEntity
    {
        public Guid FormSubtypeId { get; set; }

        public string Title { get; set; }  // e.g. Show Cause Notice (Civil)

        public string TemplateContent { get; set; } // HTML / JSON / Razor

        public bool IsEditable { get; set; } = true;

        public string Version { get; set; }

        public bool IsActive { get; set; } = true;

        // Optional mapping
        public string CaseTypeCode { get; set; }   // CIVIL / WRIT
        public string StateCode { get; set; }      // RJ / UP / SC (optional)

        public virtual FormSubtypeEntity FormSubtype { get; set; }
    }
}
