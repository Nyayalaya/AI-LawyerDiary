using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.notification_amended")]
    public class NotificationAmendedEntity : AuditableEntity
    {
        [ForeignKey("Notification_Amended")]
        public int NotificationId { get; set; }
        public int AmendedNotificationID { get; set; }
    }
}
