using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class BookMasterCacheKeys
    {
        public static string ListKey => "BookMasterList";
        public static string SelectListKey => "BookMasterSelectList";
        public static string GetKey(Guid Id) => $"BookMaster-{Id}";
        public static string GetDetailsKey(Guid Id) => $"BookMasterDetails-{Id}";
    }
}
