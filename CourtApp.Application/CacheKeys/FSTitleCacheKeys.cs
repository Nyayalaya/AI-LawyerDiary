using System;
namespace CourtApp.Application.CacheKeys
{
    public class FSTitleCacheKeys
    {
        public static string ListKey => "FSTitleList";
        public static string GetKey(Guid Id) => $"FSTitle-{Id}";        
    }
}
