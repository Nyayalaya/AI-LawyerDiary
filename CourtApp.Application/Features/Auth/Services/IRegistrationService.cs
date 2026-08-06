

using CourtApp.Application.Features.Auth.Dto;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Services
{
    public interface IRegistrationService
    {
        Task<string> RegisterAsync(RegisterRequest request);
       
    }
}
