using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_location")]
    [Index(nameof(Name), nameof(StateId), IsUnique = true)]
    public class LocationEntity : AuditableEntity
    {
        [Required]
        public required string Name { get; set; }

        public int StateId { get; set; }
        public StateEntity State { get; set; }

        public LocationType Type { get; set; }

        public Guid? ParentLocationId { get; set; }
        public LocationEntity ParentLocation { get; set; }

        public List<LangEntity> Languages { get; set; } = new();

        public ICollection<LocationEntity> Children { get; set; }

        public ICollection<CourtEntity> Courts { get; set; }
    }

}
