using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.Common
{
    [Table("m_frm_fields")]
    [Index(nameof(Code), IsUnique = true)]
    public class FormFieldsEntitys : AuditableEntity
    {
        /// <summary>
        /// this is dynamic form code. it will be unique for every form.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// It is form label name in hindi.
        /// </summary>
        public string Label_En { get; set; }
        /// <summary>
        /// It is form label name in english.
        /// </summary>
        public string Label_Hn { get; set; }
        /// <summary>
        /// It is form label data type.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// this will represent, the field display position.
        /// </summary>
        public int Display { get; set; }

        /// <summary>
        /// This will be true if the field is mandatory fields.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// This will represent the carrector of the fields.
        /// </summary>
        public int Length { get; set; }
    }
}
