using CourtApp.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Shared
{
    public interface ICurrentUserService
    {
        UserContextInfo GetCurrentUser();
        bool HasRole(string role);
        bool HasAnyRole(params string[] roles);
    }
}
