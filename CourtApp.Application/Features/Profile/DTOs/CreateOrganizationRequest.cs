using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class CreateOrganizationRequest
    {
        public string OrganizationName { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public string OwnerUserId { get; set; } // Creator (Lawyer/Corporate)
    }
}
