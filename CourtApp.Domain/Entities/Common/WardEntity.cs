using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using AuditTrail.Abstrations;
using System.Collections.Generic;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_ward")]   
    public class WardEntity
    {
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int CityId { get; set; }
        public virtual CityEntity city { get; set; }
    }
}
