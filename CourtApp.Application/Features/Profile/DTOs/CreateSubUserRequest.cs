using CourtApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class CreateSubUserRequest
    {
        // 🔹 Login Info
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        // 🔹 Basic Info
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // 🔹 Relation Type (Clerk / Junior / Assistant)
        public UserRelationType RelationType { get; set; }

        // 🔹 Optional
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }

        public string EnrollmentNumber { get; set; }
    }
}
