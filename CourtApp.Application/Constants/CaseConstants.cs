namespace CourtApp.Application.Constants
{
    /// <summary>
    /// Case status constants used across the application
    /// </summary>
    public static class CaseStatus
    {
        public const string Pending = "Pending";
        public const string Active = "Active";
        public const string OnHearing = "On Hearing";
        public const string Disposed = "Disposed";
        public const string Withdrawn = "Withdrawn";
        public const string Dismissed = "Dismissed";
        public const string Adjourned = "Adjourned";
        public const string Reserved = "Reserved";
        public const string Unknown = "Unknown";
    }

    /// <summary>
    /// Case stage constants used across the application
    /// </summary>
    public static class CaseStage
    {
        public const string Filing = "Filing";
        public const string Registration = "Registration";
        public const string FirstHearing = "First Hearing";
        public const string Hearing = "Hearing";
        public const string Arguments = "Arguments";
        public const string Judgment = "Judgment";
        public const string PostJudgment = "Post Judgment";
        public const string Appeal = "Appeal";
        public const string Unknown = "Unknown";
    }

    /// <summary>
    /// Case type constants
    /// </summary>
    public static class CaseType
    {
        public const string Civil = "Civil";
        public const string Criminal = "Criminal";
        public const string Constitutional = "Constitutional";
        public const string Administrative = "Administrative";
        public const string Commercial = "Commercial";
        public const string Constitutional_Petition = "Constitutional Petition";
        public const string Writ_Petition = "Writ Petition";
    }

    /// <summary>
    /// Court type constants
    /// </summary>
    public static class CourtType
    {
        public const string HighCourt = "High Court";
        public const string DistrictCourt = "District Court";
        public const string SupremeCourt = "Supreme Court";
        public const string SubordinateCourt = "Subordinate Court";
        public const string Tribunal = "Tribunal";
    }

    /// <summary>
    /// Case category constants
    /// </summary>
    public static class CaseCategory
    {
        public const string Original = "Original";
        public const string Appeal = "Appeal";
        public const string Revision = "Revision";
        public const string PetitionForCuration = "Petition for Curation";
        public const string Transfer = "Transfer";
    }

    /// <summary>
    /// Case importance levels
    /// </summary>
    public static class CaseImportance
    {
        public const string Normal = "Normal";
        public const string Important = "Important";
        public const string Urgent = "Urgent";
        public const string Critical = "Critical";
    }
}