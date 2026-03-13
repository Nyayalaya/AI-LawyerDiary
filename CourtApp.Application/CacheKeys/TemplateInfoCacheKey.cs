using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class TemplateInfoCacheKey
    {
        public static string ListKey => "FormList";
        public static string SelectListKey => "FormSelectList";
        public static string GetKey(Guid Id) => $"Form-{Id}";
        public static string GetDetailsKey(Guid Id) => $"FormDetails-{Id}";
    }
}
