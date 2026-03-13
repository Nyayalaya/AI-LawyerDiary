using CourtApp.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class CurrentRequestProvider : ICurrentRequestProvider
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
