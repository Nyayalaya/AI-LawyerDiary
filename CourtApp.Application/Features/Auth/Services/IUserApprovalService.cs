using CourtApp.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Services
{
    public interface IUserApprovalService
    {
        Task<string> ApproveUserAsync(string userId);
        Task<string> RejectUserAsync(string userId, string reason);
    }
}
