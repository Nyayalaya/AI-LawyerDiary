using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_c_type", Schema = "ld")]

    public class TypeOfCasesEntity : AuditableEntity
    {
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public Guid NatureId { get; set; }
        public virtual NatureEntity Nature { get; set; }
        public Guid CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
        //public int StateId { get; set; }
        //public virtual StateEntity State { get; set; }
    }
}