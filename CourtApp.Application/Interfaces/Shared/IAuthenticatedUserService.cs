using System.Collections.Generic;

namespace CourtApp.Application.Interfaces.Shared
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        List<string> UserRole { get; }
        public string Username { get; }
    }
}