using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class FmpPermissionSlipFormViewModel
    {
        public List<PermissionSlipDataModel> PerSlipInfo { get; set; }
    }

    public class PermissionSlipDataModel
    {
        /// <summary>
        /// This is Case Type property
        /// </summary>
        public string CaseType { get; set; }

        /// <summary>
        /// In this field conacted data would be display First Title Vs Secound Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Here Case Number & Year would be display as Case Number/Case Year
        /// </summary>
        public string NoYear { get; set; } 

        /// <summary>
        /// Date of filling petition/apeal/application which is prayed to be listed
        /// </summary>
        public string DoP { get; set; } 

        /// <summary>
        /// Date of passing impuged order 
        /// </summary>
        public string DoI { get; set; }

        /// <summary>
        /// Prayer is made on behalf of
        /// </summary>
        public string PrayerMadeBy { get; set; }
        /// <summary>
        /// Matter Shall go for
        /// </summary>
        public string MatterGo { get; set; }

        /// <summary>
        /// Next date when the next date has already given by Hon'ble Court
        /// </summary>
        public string NextDate { get; set; }
        /// <summary>
        /// Matter is prayed to be listed on
        /// </summary>
        public string PrayedMatter { get; set; }

    }
}
