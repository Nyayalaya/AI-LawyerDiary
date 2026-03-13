using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_city")]
    public class CityEntity
    {
       
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int DistrictId { get; set; }
        public virtual DistrictEntity District { get; set; }
        public ICollection<WardEntity> Wards { get; set; }
    }
}
