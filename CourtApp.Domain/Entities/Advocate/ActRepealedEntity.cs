using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.m_repealed_rule")]
    public class ActRepealedEntity : AuditableEntity
    {
        public int RepealedActID { get; set; }

        [ForeignKey("ActID")]
        public ActEntity Act { get; set; }
    }
}
