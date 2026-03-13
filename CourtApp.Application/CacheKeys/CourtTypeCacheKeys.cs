using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public static class CourtTypeCacheKeys
    {
        private const string Prefix = "courttype_";
        public const string ListKey = $"{Prefix}list";
        public const string DropdownKey = $"{Prefix}dropdown";

        public static string GetKey(Guid id) => $"{Prefix}{id}";
    }
}
