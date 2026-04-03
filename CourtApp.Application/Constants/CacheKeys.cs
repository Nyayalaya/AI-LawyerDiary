using System;

namespace CourtApp.Application.Constants
{
    public class CacheKeys
    {
        private static string Prefix<T>() => typeof(T).Name.ToLower();

        public static string List<T>() => $"{Prefix<T>()}_list";

        public static string Dropdown<T>() => $"{Prefix<T>()}_dropdown";

        public static string ById<T>(Guid id) => $"{Prefix<T>()}_{id}";
    }
}
