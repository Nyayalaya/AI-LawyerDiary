
using CourtApp.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class CompleteProfileRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public string  ProfileImageUrl { get; set; }
        public List<UserAddressDto> Addresses { get; set; } = new();
        public List<UserContactDto> Contacts { get; set; } = new();
        public ProfessionalInfoDto ProfessionalInfo { get; set; }
        public List<UserWorkLocationDto> WorkLocations { get; set; }
    }
}
