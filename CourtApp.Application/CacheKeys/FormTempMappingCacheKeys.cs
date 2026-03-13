using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class FormTempMappingCacheKeys
    {
        public static string ListKey => "TempFormList";        
        public static string GetKey(Guid Id) => $"TempForm-{Id}";
        public static string GetDetailsKey(Guid Id) => $"TempFormDetails-{Id}";
    }
}
