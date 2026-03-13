using System;
namespace CourtApp.Application.CacheKeys
{
    public class CourtComplexCacheKeys
    {
        public static string ListKey => "CourtComplexList";
        public static string GetByIdKey(Guid Id) => $"CourtComplex-{Id}";
    }
}
