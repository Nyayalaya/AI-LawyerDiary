using System;
namespace CourtApp.Application.CacheKeys
{
    public class SpecilityCacheKeys
    {
        public static string ListKey => "SpeciltyList"; 
        public static string GetKey(Guid Id) => $"Specilty-{Id}";
        public static string GetDetailsKey(Guid Id) => $"Specilty-{Id}";
    }
}
