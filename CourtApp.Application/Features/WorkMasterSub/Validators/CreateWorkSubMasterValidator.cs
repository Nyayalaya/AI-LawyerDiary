using FluentValidation;
using CourtApp.Application.Features.WorkMasterSub.Commands;

namespace CourtApp.Application.Features.WorkMasterSub.Validators
{
    public class CreateWorkSubMasterValidator : AbstractValidator<CreateWorkSubMasterCommand>
    {
        public CreateWorkSubMasterValidator()
        {
            RuleFor(x => x.WorkId)
                .NotEmpty().WithMessage("WorkId is required.");

            RuleFor(x => x.CourtTypeId)
                .NotEmpty().WithMessage("CourtTypeId is required.");

            RuleFor(x => x.Works)
                .NotNull().WithMessage("Works list is required.")
                .NotEmpty().WithMessage("Works list cannot be empty.");

            RuleForEach(x => x.Works).ChildRules(work =>
            {
                work.RuleFor(w => w.Name_En)
                    .NotEmpty().WithMessage("Name in English is required.")
                    .MaximumLength(255).WithMessage("Name in English cannot exceed 255 characters.");

                work.RuleFor(w => w.Name_Hn)
                    .MaximumLength(255).WithMessage("Name in Hindi cannot exceed 255 characters.");

                work.RuleFor(w => w.Abbreviation)
                    .MaximumLength(50).WithMessage("Abbreviation cannot exceed 50 characters.");
            });
        }
    }
}
