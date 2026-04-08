using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Services
{
    public interface IUserBasicInfoService
    {
        Task<bool> IsEmailExistAsync(string email);
        Task<bool> IsContactExistAsync(string contact);
        Task<bool> IsEnrollmentExistAsync(string enrollement);
        Task<bool> IsRegistrationNumberExistAsync(string registrationNumber);
    }
}
