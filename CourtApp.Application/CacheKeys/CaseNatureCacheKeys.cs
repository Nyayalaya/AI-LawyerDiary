using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class CaseNatureCacheKeys
    {
        public static string ListKey => "NatureList";
        public static string QuerableKey => "QryCaseCategory";

        public static string SelectListKey => "NatureSelectList";

        public static string GetKey(Guid natureId) => $"Nature-{natureId}";

        public static string GetDetailsKey(int natureId) => $"NatureDetails-{natureId}";
    }
}
