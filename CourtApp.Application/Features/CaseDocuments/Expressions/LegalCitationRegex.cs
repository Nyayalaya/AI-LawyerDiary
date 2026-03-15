using System.Text.RegularExpressions;

namespace CourtApp.Application.Features.CaseDocuments.Expressions
{
    public class LegalCitationRegex
    {
        public static readonly Regex SectionRegex =
       new(@"Section\s+(\d+[A-Za-z]*)\s+(IPC|CrPC|CPC|NI Act)",
       RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static readonly Regex AirRegex =
            new(@"AIR\s+(\d{4})\s+(SC|All|Bom|Del|Raj)\s+(\d+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static readonly Regex SccRegex =
            new(@"\((\d{4})\)\s+(\d+)\s+SCC\s+(\d+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
