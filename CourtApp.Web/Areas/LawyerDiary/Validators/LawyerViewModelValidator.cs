using CourtApp.Web.Areas.LawyerDiary.Models.Lawyer;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class LawyerViewModelValidator : AbstractValidator<LawyerUpsertViewModel>
    {
        public LawyerViewModelValidator()
        {
            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.Mobile)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
