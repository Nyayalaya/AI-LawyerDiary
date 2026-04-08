using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class UserWorkLocationDto
    {
        public Guid CourtId { get; set; }
        public Guid? CourtComplexId { get; set; }
        public Guid? CourtHallId { get; set; }

        public bool IsPrimary { get; set; }
    }
}
