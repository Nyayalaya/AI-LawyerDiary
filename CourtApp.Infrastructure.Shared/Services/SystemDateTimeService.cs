using CourtApp.Application.Interfaces.Shared;
using System;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}