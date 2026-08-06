using FluentValidation;
using CourtApp.Application.Features.WorkMasterSub.Commands;

namespace CourtApp.Application.Features.WorkMasterSub.Validators
{
    public class UpdateWorkSubMasterValidator : AbstractValidator<UpdateWorkSubMasterCommand>
    {
        public UpdateWorkSubMasterValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.WorkId)
                .NotEmpty().WithMessage("WorkId is required.");

            RuleFor(x => x.CourtTypeId)
                .NotEmpty().WithMessage("CourtTypeId is required.");

            RuleFor(x => x.Name_En)
                .NotEmpty().WithMessage("Name in English is required.")
                .MaximumLength(255).WithMessage("Name in English cannot exceed 255 characters.");

            RuleFor(x => x.Name_Hn)
                .MaximumLength(255).WithMessage("Name in Hindi cannot exceed 255 characters.");

            RuleFor(x => x.Abbreviation)
                .MaximumLength(50).WithMessage("Abbreviation cannot exceed 50 characters.");
        }
    }
}
