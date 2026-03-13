using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseAgainstModel
    {
        #region Common Properties
        /// <summary>
        /// This field is linked with CaseDetail Entity. 
        /// And one case may have multiple against case.
        /// </summary>
        public Guid? CaseId { get; set; }
        public DateTime? ImpugedOrderDate { get; set; }
        public Guid? CourtTypeId { get; set; }
        public Guid? CaseCategoryId { get; set; }
        public Guid? CaseTypeId { get; set; }
        public int? StateId { get; set; }
        public string CaseNo { get; set; }
        public int? CaseYear { get; set; }
        public int? CisYear { get; set; }
        public string OfficerName { get; set; }
        public Guid? CadreId { get; set; }
        public string CisNo { get; set; }
        public string CnrNo { get; set; }      
        
        #endregion

        #region Other than High Court Propeties
        public Guid? CourtDistrictId { get; set; }
        public Guid? ComplexId { get; set; }
        public Guid? CourtId { get; set; }
        #endregion

        #region HighCourt Properties
        public int? StrengthId { get; set; }
        public Guid? BenchId { get; set; }
        #endregion
        public bool IsAgHighCourt { get; set; }

        #region Select List Region
        public SelectList ACaseNatures { get; set; }
        public SelectList ATypeOfCases { get; set; }
        public SelectList ACourtTypes { get; set; }
        public SelectList ACourts { get; set; }
        public SelectList ACaseKinds { get; set; }
        public SelectList AYears { get; set; }
        public SelectList ACaseStages { get; set; }
        public SelectList AClientList { get; set; }
        public SelectList ACaseStatusList { get; set; }
        public SelectList AFirstTitleList { get; set; }
        public SelectList ASecondTitleList { get; set; }
        public SelectList ALinkedBy { get; set; }
        public SelectList ACadres { get; set; }
        public SelectList ACourtDistricts { get; set; }
        public SelectList AComplexBenchs { get; set; }
        public SelectList AStates { get; set; }
        public SelectList AStrengths { get; set; }

        #endregion

    }
}
