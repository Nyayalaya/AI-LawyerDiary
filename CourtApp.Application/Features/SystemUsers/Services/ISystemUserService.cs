using CourtApp.Application.Features.SystemUsers.DTOs;
using CourtApp.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.SystemUsers.Services
{
    /// <summary>
    /// Service interface for system user management operations
    /// Provides read and management access to directly registered users (Lawyer, Corporate, etc.)
    /// </summary>
    public interface ISystemUserService
    {
        /// <summary>
        /// Get all registered system users with optional filtering and pagination
        /// </summary>
        Task<List<SystemUserResponse>> GetAllRegisteredUsersAsync(
            RegisterType? userType = null,
            UserAccountStatus? status = null,
            int pageNumber = 1,
            int pageSize = 10,
            string searchTerm = null
        );

        /// <summary>
        /// Get system user by user ID (ApplicationUser ID)
        /// </summary>
        Task<SystemUserResponse> GetSystemUserByIdAsync(string userId);

        /// <summary>
        /// Update system user subscription and status
        /// </summary>
        Task<bool> UpdateSystemUserAsync(string userId, UpdateSystemUserRequest request);

        /// <summary>
        /// Get users count by type
        /// </summary>
        Task<Dictionary<RegisterType, int>> GetUserCountByTypeAsync();

        /// <summary>
        /// Get active users count
        /// </summary>
        Task<int> GetActiveUsersCountAsync();
    }
}
