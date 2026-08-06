using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class UserOrganizationRequest
    {
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string Role { get; set; } // Admin / Member / Viewer
    }
}
