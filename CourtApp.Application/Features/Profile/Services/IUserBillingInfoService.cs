

using CourtApp.Application.Features.Profile.DTOs;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Services
{
    public interface IUserBillingInfoService
    {
        Task<string> SaveBillingInfo(string userId, UserBillingInfoDto request);
        Task<string> UpdateBillingInfo(string userId, UserBillingInfoDto request);
    }
}
