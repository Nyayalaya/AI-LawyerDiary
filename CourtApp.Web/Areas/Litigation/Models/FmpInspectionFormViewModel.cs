using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class FmpInspectionFormViewModel
    {
        public List<InspectionViewModel> Cases { get; set; }
    }
    public class InspectionViewModel
    {
        
        /// <summary>
        /// It is concatenation property of Case Number & Case Year
        /// </summary>
        public string NoYear { get; set; }
        /// <summary>
        /// It is high court bench
        /// </summary>
        public string Bench { get; set; }

        /// <summary>
        /// Court Type
        /// </summary>
        public string CourtType { get; set; }
        /// <summary>
        /// It is Case type property
        /// </summary>
        public string CaseType { get; set; }

        /// <summary>
        /// It is combined property of First & Second Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// It is next hearing date 
        /// </summary>
        public string NextDate { get; set; }

        /// <summary>
        /// Court Name
        /// </summary>
        public string CourtName { get; set; }

        /// <summary>
        /// This property is getting from client appearence property
        /// </summary>
        public string Appearence { get; set; }
    }
}
