using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class FmpCopyingAppViewModel
    {
        public List<CopyingAppViewModel> Cases { get; set; }
    }
    public class CopyingAppViewModel
    {
        /// <summary>
        /// Case Type
        /// </summary>
        public string CaseType { get; set; }

        /// <summary>
        /// Case Number & Year concatenation
        /// </summary>
        public string NoYear { get; set; }

        /// <summary>
        /// Case title First title 
        /// </summary>
        public string FirstTitle { get; set; }
        /// <summary>
        /// Case title Second title 
        /// </summary>
        public string SecondTitle { get; set; }

        /// <summary>
        /// If Next Date Available, will display next        
        /// </summary>
        public string NextDate { get; set; }

        /// <summary>
        /// this property will display the case court
        /// </summary>
        public string CourtType { get; set; }

        /// <summary>
        /// Court Name in case of High Court Bench Name would come.
        /// </summary>
        public string Court { get; set; }

        /// <summary>
        /// At the time client add appearence value will come here.
        /// </summary>
        public string Appearence { get; set; }

        /// <summary>
        /// Who is layer of this Case
        /// </summary>
        public string LawyerName { get; set; }

        /// <summary>
        /// Lawyer Address will come here.
        /// </summary>
        public string LawyerAddress { get; set; }

        /// <summary>
        /// If case is already disposed, disposal date will appear
        /// </summary>
        public string DisposalDate { get; set; }
    }
}
