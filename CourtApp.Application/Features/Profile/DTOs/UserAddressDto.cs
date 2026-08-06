using CourtApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class UserAddressDto
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public int StateId { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }

        public AddressType Type { get; set; }

        public bool IsPrimary { get; set; }   // ⭐ Important
    }
}
