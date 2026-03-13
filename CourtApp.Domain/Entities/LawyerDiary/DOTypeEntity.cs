using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_do_type", Schema = "ld")]
    public class DOTypeEntity : AuditableEntity
    {
        public int TypeId { get; set; }
        public string Name_En { get; set; }
    }
}
