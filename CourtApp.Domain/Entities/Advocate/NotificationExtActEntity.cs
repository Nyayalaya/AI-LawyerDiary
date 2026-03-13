using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.notification_ext_act")]
    public class NotificationExtActEntity: AuditableEntity
    {
        public int NotificationId { get; set; }
        public int ActId { get; set; }
    }
}
