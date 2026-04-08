
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Permission.Services
{
    public interface IRolePermissionService
    {
        Task<bool> HasRolePermissionAsync(string userId, string permission);
    }
}
