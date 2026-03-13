using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class WorkTypeModelValidator : AbstractValidator<WorkMasterViewModel>
    {
        public WorkTypeModelValidator()
        {

            RuleFor(p => p.Work_En)
              .NotEmpty().WithMessage("Please enter work name.")
              .NotNull()
              .Matches(@"^(?!'+$)[a-zA-Z']+(?:\s+[a-zA-Z']+)*$").WithMessage("Special charector is not allowed!");

            //RuleFor(p => p.Abbreviation)
            // .NotEmpty().WithMessage("Please enter type abbreviation.")
            // .NotNull()
            // .Matches(@"^(?!'+$)[a-zA-Z']+(?:\s+[a-zA-Z']+)*$").WithMessage("Special charector is not allowed!");
        }
    }
}
