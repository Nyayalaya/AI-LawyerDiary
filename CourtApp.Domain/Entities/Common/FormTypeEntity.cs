using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_form_type")]
    [Index(nameof(Code), IsUnique = true)]
    public class FormTypeEntity : AuditableEntity
    {
        /// <summary>
        /// This is related to unique form field code.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// This is type name of the form such as accident form
        /// </summary>
        public string Name { get; set; }
    }
}
