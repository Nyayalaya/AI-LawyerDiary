using System;

namespace CourtApp.Application.CacheKeys
{
    public class FormBuilderCacheKeys
    {
        public static string ListKey => "FormList";
        public static string SelectListKey => "FormSelectList";
        public static string GetKey(Guid Id) => $"Form-{Id}";
        public static string GetDetailsKey(Guid Id) => $"FormDetails-{Id}";
    }
}
