using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public static class CourtLevelCacheKeys
    {
        private static readonly string Prefix = nameof(CourtLevelCacheKeys);
        public static readonly string DropdownKey = $"{Prefix}_Dropdown";
        
        public static string GetKey(string suffix)
            => $"{Prefix}_{suffix}";
    }
}
