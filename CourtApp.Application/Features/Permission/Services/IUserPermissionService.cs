
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Permission.Services
{
    public interface IUserPermissionService
    {
        Task<bool?> HasUserPermissionAsync(string userId, string permission);
    }
}
