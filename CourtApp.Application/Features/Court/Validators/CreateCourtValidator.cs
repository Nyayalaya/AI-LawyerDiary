using CourtApp.Application.Features.Court.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Court.Validators
{
    public class CreateCourtValidator : AbstractValidator<CreateCourtCommand>
    {
        public CreateCourtValidator()
        {
            

            RuleFor(x => x.Courts)
                .NotNull().WithMessage("Courts list is required")
                .NotEmpty().WithMessage("At least one court must be provided");

            RuleForEach(x => x.Courts).ChildRules(court =>
            {
                court.RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Court name is required")
                    .MaximumLength(255).WithMessage("Court name must not exceed 255 characters");

               

                court.RuleFor(x => x.CourtLevelId)
                    .NotEmpty().WithMessage("Court level ID is required");
            });
        }
    }
}
