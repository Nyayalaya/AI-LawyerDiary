using System;

namespace CourtApp.Application.CacheKeys
{
    public static class LocationCacheKeys
    {
        private const string Prefix = "Location";

        public static string LocationKey(Guid id) => $"{Prefix}_{id}";
        public static string LocationByStateKey(int stateId) => $"{Prefix}_State_{stateId}";
    }
}
