using CourtApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class UserContactDto
    {
        public ContactType ContactType { get; set; }

        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public bool IsPrimary { get; set; }
    }
}
