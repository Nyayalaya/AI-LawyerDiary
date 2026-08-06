using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Services
{
    public interface IUserProfileService
    {
        Task<string> CompleteProfileAsync(string userId, CompleteProfileRequest request);
        Task<UserProfileDto> GetProfileAsync(string userId);
        Task<string> UpdateProfileAsync(string userId, UpdateProfileRequest request);
    }
}
