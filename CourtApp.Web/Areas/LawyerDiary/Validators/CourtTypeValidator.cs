using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class CourtTypeValidator : AbstractValidator<CourtTypeViewModel>
    {
        public CourtTypeValidator()
        {
            RuleFor(p => p.CourtType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Abbreviation)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(4).WithMessage("{PropertyName} must not exceed 4 characters.");
        }
    }
}
