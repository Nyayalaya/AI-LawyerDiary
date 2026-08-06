using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_form", Schema = "masters")]
    public class FormMasterEntity : AuditableEntity
    {
        public Guid FormTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual FormTypeEntity FormType { get; set; }
    }
}
