using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Services
{
    public interface IUserOrganizationService
    {
        Task<Result<string>> CreateOrganizationAsync(CreateOrganizationRequest request);
        Task<Result<string>> MapUserAsync(UserOrganizationRequest request);
        Task<Result<List<UserDto>>> GetOrganizationUsersAsync(string orgId);
    }
}
