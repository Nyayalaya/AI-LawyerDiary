using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.m_repealed_notification")]
    public class NotificationRepealedEntity : AuditableEntity
    {
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public int RepealedNotificationID { get; set; }
    }
}
