using CourtApp.Web.Areas.LawyerDiary.Models.Title;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class TitleViewModelValidator:AbstractValidator<TitleViewModel>
    {
        public TitleViewModelValidator()
        {
            RuleFor(p => p.TypeId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.CaseId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.CaseApplicants)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
        }
    }
}
