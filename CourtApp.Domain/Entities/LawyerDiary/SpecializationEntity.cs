using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("specilization", Schema = "ld")]
    public class SpecializationEntity:AuditableEntity
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Description { get; set; }
    }
}
