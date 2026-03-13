using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class TypeOfCasesCacheKeys
    {
        public static string ListKey => "TypeofcasesList";

        public static string SelectListKey => "TypeofcasesSelectList";

        public static string GetKey(Guid Id) => $"Typeofcases-{Id}";

        public static string GetDetailsKey(Guid Id) => $"TypeofcasesDetails-{Id}";
    }
}
