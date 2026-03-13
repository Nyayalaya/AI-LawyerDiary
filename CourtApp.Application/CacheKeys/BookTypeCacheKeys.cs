using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class BookTypeCacheKeys
    {
        public static string ListKey => "BookTypeList";

        public static string SelectListKey => "BookTypeSelectList";

        public static string GetKey(Guid Id) => $"BookType-{Id}";

        public static string GetDetailsKey(Guid Id) => $"BookTypeDetails-{Id}";
    }
}
