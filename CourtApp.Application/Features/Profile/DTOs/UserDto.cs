using CourtApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RelationType { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
