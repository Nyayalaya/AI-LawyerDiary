
using System.Collections.Generic;


namespace CourtApp.Application.Features.Profile.DTOs
{
    public class UserProfileDto
    {
        public string UserId { get; set; }

        // Basic Info
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}".Replace("  ", " ").Trim();

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }

        // Address
        public List<UserAddressDto> Addresses { get; set; }

        // Contact
        public List<UserContactDto> Contacts { get; set; }

        // Professional Info (Lawyer only)
        public ProfessionalInfoDto ProfessionalInfo { get; set; }

        // Work Locations
        public List<UserWorkLocationDto> WorkLocations { get; set; } = new();

        // Organization (Corporate)
        public string OrganizationName { get; set; }
        public bool IsOrganizationOwner { get; set; }

        // Hierarchy Info
        public string ParentUserId { get; set; }
        public string Role { get; set; }   // Lawyer / Clerk / Associate / Corporate

       
    }
}
