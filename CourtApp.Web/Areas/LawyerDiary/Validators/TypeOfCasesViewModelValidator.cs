using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;
namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class TypeOfCasesViewModelValidator : AbstractValidator<TypeOfCasesViewModel>
    {
        public TypeOfCasesViewModelValidator()
        {
            RuleFor(c => c.CourtTypeId)
                .NotEmpty().WithMessage("Court type is required.")
                .NotNull();
            RuleFor(c => c.NatureId)
               .NotEmpty().WithMessage("Court category is required.")
               .NotNull();

            //RuleFor(x => x.CaseTypes)
            //.NotEmpty().WithMessage("At least one case type is required.");

            // Inline validation for CaseTypes (replaces external CaseTypeValidator)
            //RuleForEach(x => x.CaseTypes).ChildRules(caseType =>
            //{
            //    caseType.RuleFor(ct => ct.Name_En)
            //        .NotEmpty().WithMessage("Case Type Name is required.")
            //        .MaximumLength(100).WithMessage("Name can't exceed 100 characters.");

            //    caseType.RuleFor(ct => ct.Abbreviation)
            //        .NotNull().WithMessage("Abbreviation must be specified.");
            //});


            //RuleFor(p => p.Name_En)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            //RuleFor(p => p.Abbreviation)
            //.NotEmpty().WithMessage("Please enter abbreviation.")
            //.NotNull()
            //.Matches(@"^(?!'+$)[a-zA-Z']+(?:\s+[a-zA-Z']+)*$").WithMessage("Special charector is not allowed!");

        }
    }
}
