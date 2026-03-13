using CourtApp.Web.Areas.LawyerDiary.Models.Title;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class FSTitleMViewValidator : AbstractValidator<FSTitleMViewModel>
    {
        public FSTitleMViewValidator()
        {
            RuleFor(p => p.TypeId)
              .NotEmpty().WithMessage("Title type is required.")
              .NotNull();
            RuleFor(p => p.Name_En)
              .NotEmpty().WithMessage("Title name is required.")
              .NotNull();
        }
    }
}
