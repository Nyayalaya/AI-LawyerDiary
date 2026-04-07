using System;

namespace CourtApp.Application.CacheKeys
{
    public static class CourtCacheKeys
    {
        private const string Prefix = "Court";

        public static string CourtKey(Guid id) => $"{Prefix}_{id}";
        public static string CourtByLocationKey(Guid locationId) => $"{Prefix}_Location_{locationId}";
    }
}
