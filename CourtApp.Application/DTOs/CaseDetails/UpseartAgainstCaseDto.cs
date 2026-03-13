using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseDetails
{
    public class UpseartAgainstCaseDto
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
        public string Cadre { get; set; }
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
    }
}
