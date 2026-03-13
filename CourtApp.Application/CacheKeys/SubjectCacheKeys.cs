using System;


namespace CourtApp.Application.CacheKeys
{
   public class SubjectCacheKeys
    {
        public static string ListKey => "SubjectList";

        public static string SelectListKey => "SubjectSelectList";

        public static string GetKey(Guid brandId) => $"Subject-{brandId}";

        public static string GetDetailsKey(Guid brandId) => $"SubjectDetails-{brandId}";
    }
}
