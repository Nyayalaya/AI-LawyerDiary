using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using CourtApp.Application.Features.Profile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class UserOrganizationService : IUserOrganizationService
    {
        public Task<Result<string>> CreateOrganizationAsync(CreateOrganizationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<UserDto>>> GetOrganizationUsersAsync(string orgId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> MapUserAsync(UserOrganizationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
