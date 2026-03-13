using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.m_notification")]
    public class NotificationTypeEntity : AuditableEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
    }
}
