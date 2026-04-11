using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_form_type")]
    [Index(nameof(Code), IsUnique = true)]
    public class FormTypeEntity : AuditableEntity
    { 
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
