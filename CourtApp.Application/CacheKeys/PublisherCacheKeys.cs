using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class PublisherCacheKeys
    {
        public static string ListKey => "PublisherList";

        public static string SelectListKey => "PublisherSelectList";

        public static string GetKey(Guid brandId) => $"Publisher-{brandId}";

        public static string GetDetailsKey(int brandId) => $"PublisherDetails-{brandId}";
    }
}
