using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class CadreMasterCacheKeys
    {
        public static string ListKey => "CadreMasterList";
        public static string SelectListKey => "CadreMasterList";
        public static string GetKey(Guid Id) => $"CadreMaster-{Id}";
        public static string GetDetailsKey(Guid Id) => $"CadreMasterDetail-{Id}";
    }
}
