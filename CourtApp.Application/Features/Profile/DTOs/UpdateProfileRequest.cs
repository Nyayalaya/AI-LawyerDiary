using CourtApp.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class UpdateProfileRequest
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string UserId { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }

        public string ProfileImageUrl { get; set; }

        public string PhoneNumber { get; set; }

        public List<UserAddressDto> Addresses { get; set; } = new();
        public List<UserContactDto> Contacts { get; set; } = new();
        public ProfessionalInfoDto ProfessionalInfo { get; set; }
        public List<UserWorkLocationDto> WorkLocations { get; set; }
    }
}
