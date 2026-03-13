using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.Litigation.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class BringCaseValidator : AbstractValidator<BringCaseViewModel>
    {
        public BringCaseValidator()
        {
            RuleFor(p => p.LawyerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                ;
            RuleFor(p => p.CaseId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                ;

        }
    }
}
