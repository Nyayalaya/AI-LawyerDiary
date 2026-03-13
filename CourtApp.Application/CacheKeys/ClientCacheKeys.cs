using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class ClientCacheKeys
    {
        public static string ListKey => "ClientList";

        public static string SelectListKey => "ClientSelectList";

        public static string GetKey(Guid clientId) => $"Client-{clientId}";

        public static string GetDetailsKey(Guid clientId) => $"ClientDetails-{clientId}";
    }
}
