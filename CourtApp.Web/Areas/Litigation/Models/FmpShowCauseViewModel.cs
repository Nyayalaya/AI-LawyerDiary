using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class FmpShowCauseViewModel
    {
        public List<ShowCauseViewModel> ShowCauses { get; set; }
    }
    public class ShowCauseViewModel
    {
        /// <summary>
        /// Petition information
        /// </summary>
        public string Petitioner { get; set; }

        /// <summary>
        /// Respondent Information
        /// </summary>
        public string Respondent { get; set; }

        /// <summary>
        /// Combination of Case No & Year
        /// </summary>
        public string CaseNoYear { get; set; }

        /// <summary>
        /// Case Type
        /// </summary>
        public string CaseType { get; set; }

        /// <summary>
        /// First Complete Title
        /// </summary>
        public string FirstTitle { get; set; }

        /// <summary>
        /// First Title Complete Address
        /// </summary>
        public string FAddress { get; set; }

        /// <summary>
        /// Second Complete Title
        /// </summary>
        public string SecondTitle { get; set; }
        /// <summary>
        /// Second Title Complete Address
        /// </summary>
        public string SAddress { get; set; }

        public List<ApplicantDetailViewModel> Applicants { get; set; }
    }
}
