using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_form_subtype")]
    public class FormSubtypeEntity : AuditableEntity
    {
        public Guid FormId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual FormMasterEntity Form { get; set; }
    }
}
