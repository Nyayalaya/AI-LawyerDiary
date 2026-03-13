using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class DOTypeCacheKeys
    {
        public static string ListKey => "DOTypeList";
        public static string SelectListKey => "DOTypeSelectList";
        public static string GetKey(Guid Id) => $"DOType-{Id}";
        public static string GetDetailsKey(Guid Id) => $"DOType-{Id}";
    }
}
