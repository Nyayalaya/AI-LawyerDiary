using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class CaseKindCacheKeys
    {
        public static string ListKey => "CaseKindList";
        public static string ListKeyQry => "CaseKindQry";

        public static string SelectListKey => "CaseKindSelectList";

        public static string GetKey(Guid Id) => $"CaseKind-{Id}";

        public static string GetDetailsKey(Guid Id) => $"CaseKindDetails-{Id}";
    }
}
