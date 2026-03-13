using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.CacheKeys
{
    public class CaseStageCacheKeys
    {
        public static string ListKey => "CaseStageList";

        public static string SelectListKey => "CaseStageSelectList";

        public static string GetKey(Guid Id) => $"CaseStage-{Id}";

        public static string GetDetailsKey(Guid Id) => $"CaseStageDetails-{Id}";
    }
}
