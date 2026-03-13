using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class CourtFeeStructureCacheKey
    {
        public static string ListKey => "CourtFeeStructList";

        public static string SelectListKey => "CourtFeeStructSelectList";

        public static string GetKey(Guid Id) => $"CourtFeeStruct-{Id}";

        public static string GetDetailsKey(Guid Id) => $"CourtFeeStructDetails-{Id}";
    }
}
