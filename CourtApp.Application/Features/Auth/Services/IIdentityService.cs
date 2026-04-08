
using System.Threading.Tasks;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Dto;

namespace CourtApp.Application.Features.Auth.Services
{
    public interface IIdentityService
    {
        Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);
        Task<Result<string>> ConfirmEmailAsync(string userId, string code);
        Task<Result<string>> ForgotPasswordAsync(ForgotPasswordRequest model, string origin);
        Task<Result<string>> ResetPasswordAsync(ResetPasswordRequest model);
        

    }
}