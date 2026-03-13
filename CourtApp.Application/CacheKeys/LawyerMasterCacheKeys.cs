using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class LawyerMasterCacheKeys
    {
        public static string ListKey => "LawyerMasterList";       
        public static string GetKey(Guid Id) => $"LawyerMaster-{Id}";
        public static string GetDetailsKey(Guid Id) => $"LawyerMasterDetails-{Id}";
    }
}
