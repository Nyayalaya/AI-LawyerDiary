using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class CadreValidator : AbstractValidator<CadreMasterViewModel>
    {
        public CadreValidator()
        {
            RuleFor(p => p.Name_En)
               .NotEmpty().WithMessage("Cadre name is required.")
               .NotNull();

        }
    }
}
