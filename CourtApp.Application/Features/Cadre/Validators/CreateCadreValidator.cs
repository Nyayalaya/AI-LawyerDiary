using CourtApp.Application.Features.Cadre.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Cadre.Validators
{
    public class CreateCadreValidator : AbstractValidator<CreateCadreCommand>
    {
        public CreateCadreValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("English name is required")
                .MaximumLength(250).WithMessage("English name cannot exceed 250 characters");

            RuleFor(x => x.Code)
                .MaximumLength(250).WithMessage("Hindi name cannot exceed 250 characters");
        }
    }
}
