using CourtApp.Application.Features.CaseDetails.Commands;
using FluentValidation;
namespace CourtApp.Application.Features.CaseDetails.Validators
{
    public class UpdateCaseCommandValidator
    : AbstractValidator<UpdateCaseCommand>
    {
        public UpdateCaseCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Case.CourtTypeId)
                .NotEmpty();

            RuleFor(x => x.Case.CaseCategoryId)
                .NotEmpty();

            RuleFor(x => x.Case.FirstTitleId)
                .NotEmpty();

            RuleFor(x => x.Case.SecondTitleId)
                .NotEmpty();
        }
    }
}
