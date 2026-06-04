using System;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    /// <summary>
    /// Generic base DTO for case basic information
    /// Contains essential case details that are used across multiple features
    /// Provides a consistent structure for case data transfers
    /// </summary>
    public class CaseBasicInfoDto
    {
        /// <summary>
        /// Primary identifier for the case
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Case registration/filing date in format dd/MM/yyyy
        /// </summary>
        public string InstitutionDate { get; set; }

        /// <summary>
        /// Case title (plaintiff vs defendant)
        /// </summary>
        public string CaseTitle { get; set; }

        /// <summary>
        /// Case number combined with year (e.g., "2023-001")
        /// </summary>
        public string CaseNumberYear { get; set; }

        /// <summary>
        /// Full court name/identifier
        /// </summary>
        public string Court { get; set; }

        /// <summary>
        /// Category of case (e.g., Civil, Criminal, etc.)
        /// </summary>
        public string CaseType { get; set; }

        /// <summary>
        /// Current stage of the case in legal proceedings
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// Current status of the case
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Next hearing date or court date
        /// </summary>
        public string NextDate { get; set; }

        /// <summary>
        /// Parent case ID if this is a child/linked case
        /// </summary>
        public Guid? ParentCaseId { get; set; }

        /// <summary>
        /// Indicates if the case has child/linked cases
        /// </summary>
        public bool HasChildCases { get; set; }

        /// <summary>
        /// Reference number (CNR, CIS, etc.)
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Case number without year
        /// </summary>
        public string CaseNumber { get; set; }

        /// <summary>
        /// Year of case filing
        /// </summary>
        public int CaseYear { get; set; }

        /// <summary>
        /// Court district/location
        /// </summary>
        public string CourtDistrict { get; set; }

        /// <summary>
        /// Court complex/bench identifier
        /// </summary>
        public string CourtComplex { get; set; }

        /// <summary>
        /// Indicates if case is marked as important
        /// </summary>
        public bool IsImportant { get; set; }

        /// <summary>
        /// Indicates if case is marked as urgent
        /// </summary>
        public bool IsUrgent { get; set; }

        /// <summary>
        /// Indicates if case has been disposed/closed
        /// </summary>
        public bool IsDisposed { get; set; }
    }
}
