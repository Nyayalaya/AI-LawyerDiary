using System;

namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    /// <summary>
    /// Simplified case response DTO for quick API responses
    /// Contains essential case information for display in lists and cards
    /// Extends CaseBasicInfoDto for consistent structure
    /// </summary>
    public class CaseResponseDto : CaseBasicInfoDto
    {
        // Inherits all properties from CaseBasicInfoDto
        // All properties are properly inherited and documented in the base class
    }

    /// <summary>
    /// Alternative: Minimal case response for list views
    /// Use this when only essential information is needed
    /// </summary>
    public class CaseMinimalResponseDto
    {
        /// <summary>
        /// Case identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Case title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Combined case number and year
        /// </summary>
        public string CaseNumber { get; set; }

        /// <summary>
        /// Court name
        /// </summary>
        public string CourtName { get; set; }

        /// <summary>
        /// Case category/type
        /// </summary>
        public string CaseCategory { get; set; }

        /// <summary>
        /// Next hearing date
        /// </summary>
        public DateTime? NextDate { get; set; }

        /// <summary>
        /// Is case disposed/closed
        /// </summary>
        public bool IsDisposed { get; set; }

        /// <summary>
        /// Is case marked important
        /// </summary>
        public bool IsImportant { get; set; }

        /// <summary>
        /// Is case marked urgent
        /// </summary>
        public bool IsUrgent { get; set; }

        /// <summary>
        /// Current case status
        /// </summary>
        public string Status { get; set; }
    }
}
