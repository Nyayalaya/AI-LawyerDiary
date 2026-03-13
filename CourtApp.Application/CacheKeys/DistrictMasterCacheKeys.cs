using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class DistrictMasterCacheKeys
    {
        public static string SelectListByStateKey(int StateCode) => $"DistrictSelectList-{StateCode}";
         
    }
}