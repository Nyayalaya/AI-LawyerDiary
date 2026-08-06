using CourtApp.Application.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Services
{
    public class SeedAuthenticatedUserService : IAuthenticatedUserService
    {
        public string UserId => "00000000-0000-0000-0000-000000000001"; // dummy GUID

        public List<string> UserRole => throw new NotImplementedException();

        public string Username => throw new NotImplementedException();
    }
}
