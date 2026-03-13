using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class CourtComplexModelValidator:AbstractValidator<Complex>
    {
        public CourtComplexModelValidator()
        {
            RuleFor(p => p.Name_En)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                ;           
        }
    }
}
