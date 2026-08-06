
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Services
{
    public interface IChangePasswordService
    {
        Task<string> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
