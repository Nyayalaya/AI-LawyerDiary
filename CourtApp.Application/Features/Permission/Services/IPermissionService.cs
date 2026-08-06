
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Permission.Services
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(string userId, string permission);
    }
}
