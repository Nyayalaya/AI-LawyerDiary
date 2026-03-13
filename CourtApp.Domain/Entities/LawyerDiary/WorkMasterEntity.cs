using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_work_master", Schema = "ld")]
    public class WorkMasterEntity : AuditableEntity
    {
        public required string Work_En { get; set; }
        public string Work_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
}
