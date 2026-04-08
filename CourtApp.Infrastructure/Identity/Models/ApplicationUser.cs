using CourtApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CourtApp.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {  
        public RegisterType UserType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? EnrollmentNumber { get; set; }     
        public string? RegistrationNumber { get; set; }   
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; } = false;
        public string ProfileImageUrl { get; set; }

        // 🔹 Navigation
        public ICollection<UserAddress> Addresses { get; set; }
        public ICollection<UserContact> Contacts { get; set; }

        public ProfessionalInfoEntity ProfessionalInfo { get; set; }

        public ICollection<UserCourtMapping> UserCourts { get; set; }

        // 🔥 SELF-REFERENCE
        public ICollection<UserHierarchy> Parents { get; set; }
        public ICollection<UserHierarchy> Children { get; set; }
        public ICollection<UserOrganizationMapping> Organizations { get; set; }
    }
}