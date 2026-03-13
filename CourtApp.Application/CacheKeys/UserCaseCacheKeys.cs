using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class UserCaseCacheKeys
    {
        public static string ListKey => "UserList";
        public static string GetKey(Guid Id) => $"User-{Id}";
        public static string GetDetailsKey(Guid CaseUid) => $"UserDetails-{CaseUid}";
    }
}
