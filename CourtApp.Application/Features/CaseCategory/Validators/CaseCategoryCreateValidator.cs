using CourtApp.Application.Features.CaseCategory.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.CaseCategory.Validators
{
    public class CaseCategoryCreateValidator:AbstractValidator<CaseCategoryCreateCommand>
    {
        public CaseCategoryCreateValidator()
        {
            RuleFor(x => x.Name_En).NotEmpty().WithMessage("Name in English is required.");
            RuleFor(x => x.Name_Hn).NotEmpty().WithMessage("Name in Hindi is required.");
            RuleFor(x => x.CourtTypeId).NotEmpty().WithMessage("Court Type Id is required.");
        }
    }
}
