

namespace CourtApp.Application.CacheKeys
{
    public class AppCacheKeys
    {
        public static string CourtMasterKey => "CourtList";
        public static string CourtDistrictKey => "CourtMaster";
        public static string CourtComplexKey => "CourtComplex";       
        public static string ProcHeadKey => "ProcHead";       
        public static string SubProcHeadKey => "SubProcHead";
        public static string WorkKey => "Work";
        public static string SubWorkKey => "SubWork";
        public static string CourtFeeKey => "CourtFee";

        // 🔹 Form Management Cache Keys
        public static string FormTypeListKey => "formtype_list_key";
        public static string FormTypeByIdKey => "formtype_by_id_{0}";
        public static string FormTypeByCodeKey => "formtype_by_code_{0}";
    }
}
