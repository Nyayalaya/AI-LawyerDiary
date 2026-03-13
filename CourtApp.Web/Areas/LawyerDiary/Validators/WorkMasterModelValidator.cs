using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class WorkMasterModelValidator:AbstractValidator<WorkSubMaster>
    {
        public WorkMasterModelValidator()
        {
            //RuleFor(p => p.WorkId)
            //  .NotEmpty().WithMessage("Please select work type.")
            //  .NotNull();
            RuleFor(p => p.Name_En)
              .NotEmpty().WithMessage("Please enter work name.")
              .NotNull()
              .Matches(@"^(?!'+$)[a-zA-Z']+(?:\s+[a-zA-Z']+)*$").WithMessage("Special charector is not allowed!");

            //RuleFor(p => p.Abbreviation)
            // .NotEmpty().WithMessage("Please enter work abbreviation.")
            // .NotNull()
            // .Matches(@"^(?!'+$)[a-zA-Z']+(?:\s+[a-zA-Z']+)*$").WithMessage("Special charector is not allowed!");
        }
    }
}
