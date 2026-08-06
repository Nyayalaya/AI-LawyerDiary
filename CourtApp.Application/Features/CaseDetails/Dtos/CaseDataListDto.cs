namespace CourtApp.Application.Features.CaseDetails.Dtos
{
    /// <summary>
    /// Extended case DTO with assignment information
    /// Used when listing cases assigned to specific lawyers/users
    /// </summary>
    public class CaseDataListDto : CaseBasicInfoDto
    {
        /// <summary>
        /// ID of the assigned lawyer/user
        /// </summary>
        public string AssignedLawyerId { get; set; }

        /// <summary>
        /// Name of the assigned lawyer/user for display purposes
        /// </summary>
        public string AssignedLawyerName { get; set; }
    }
}
