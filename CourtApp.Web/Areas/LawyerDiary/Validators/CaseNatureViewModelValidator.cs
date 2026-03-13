using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class CaseNatureViewModelValidator : AbstractValidator<CaseNatureViewModel>
    {
        public CaseNatureViewModelValidator()
        {

            RuleFor(p => p.Name_En)
                .NotEmpty().WithMessage("Nature name is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("Nature must not exceed 50 characters.");
           
        }
    }
}
