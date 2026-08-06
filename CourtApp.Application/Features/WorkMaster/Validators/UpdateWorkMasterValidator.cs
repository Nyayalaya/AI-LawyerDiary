using FluentValidation;
using CourtApp.Application.Features.WorkMaster.Commands;

namespace CourtApp.Application.Features.WorkMaster.Validators
{
    public class UpdateWorkMasterValidator : AbstractValidator<UpdateWorkMasterCommand>
    {
        public UpdateWorkMasterValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Name_En)
                .NotEmpty().WithMessage("Name in English is required.")
                .MaximumLength(255).WithMessage("Name in English cannot exceed 255 characters.");

            RuleFor(x => x.Name_Hn)
                .MaximumLength(255).WithMessage("Name in Hindi cannot exceed 255 characters.");

            RuleFor(x => x.Abbreviation)
                .MaximumLength(50).WithMessage("Abbreviation cannot exceed 50 characters.");

            RuleFor(x => x.CourtTypeId)
                .NotEmpty().WithMessage("CourtTypeId is required.");
        }
    }
}
