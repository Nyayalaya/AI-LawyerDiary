using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("m_act_amended", Schema = "ad")]
    public class ActAmendedEntity : AuditableEntity
    {
        public int AmendedActID { get; set; }

        [ForeignKey("ActID")]
        public ActEntity Act { get; set; }
    }
}
