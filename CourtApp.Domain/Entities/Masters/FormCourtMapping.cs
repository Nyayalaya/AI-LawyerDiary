using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_form_court")]
    public class FormCourtMapping:AuditableEntity
    {
        public Guid FormSubtypeId { get; set; }
        public Guid CourtTypeId { get; set; }
        public bool IsMandatory { get; set; }
    }
}
