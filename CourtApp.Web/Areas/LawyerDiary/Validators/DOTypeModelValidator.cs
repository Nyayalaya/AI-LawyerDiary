using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class DOTypeModelValidator : AbstractValidator<DOTypeViewModel>
    {
        public DOTypeModelValidator()
        {
            RuleFor(p => p.TypeId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.Name_En)
              .NotEmpty().WithMessage("DO Type is required.")
              .NotNull();
        }
    }
}
