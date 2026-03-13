using Microsoft.AspNetCore.Http;

namespace CourtApp.Infrastructure.Shared.Services
{
    internal class CurrentRequestProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentRequestProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetOrigin()
        {
            var request = _httpContextAccessor?.HttpContext?.Request;
            if (request == null)
                return "http://localhost";

            return $"{request.Scheme}://{request.Host}";
        }

        public string GetIpAddress()
        {
            return _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
        }
    }
}
