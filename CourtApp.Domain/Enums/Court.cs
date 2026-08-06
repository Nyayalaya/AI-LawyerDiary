using System.ComponentModel.DataAnnotations;
namespace CourtApp.Application.Enums
{
    public enum CaseNature
    {
        [Display(Name = "Civil Cases")]
        CivilCases,
        [Display(Name = "Consumer Cases")]
        ConsumerCases,
        [Display(Name = "Company Cases")]
        CompanyCases,
        [Display(Name = "Criminal Cases")]
        CriminalCases,
        [Display(Name = "Other Matters")]
        OtherMatters,
        [Display(Name = "Revenu Matters")]
        RevenueMatters,
        [Display(Name = "Service Matters")]
        ServiceMatters
    }
}