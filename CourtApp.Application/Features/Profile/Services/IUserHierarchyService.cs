using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Services
{
    public interface IUserHierarchyService
    {
        Task<string> CreateSubUserAsync(CreateSubUserRequest request, string parentUserId);
        Task<List<UserDto>> GetSubUsersAsync(string userId);
        Task<string> RemoveSubUserAsync(string userId);
        Task<List<string>> GetAllParentIdsAsync(string userId);
        Task<List<string>> GetAllChildIdsAsync(string userId);
        Task<bool> IsParentAsync(string parentId, string childId);
        Task<bool> IsChildAsync(string childId, string parentId);
    }
}
