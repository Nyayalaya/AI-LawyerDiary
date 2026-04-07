using CourtApp.Application.Features.Cadre.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Cadre.Validators
{
    public class UpdateCadreValidator : AbstractValidator<UpdateCadreCommand>
    {
        public UpdateCadreValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Cadre ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("English name is required")
                .MaximumLength(250).WithMessage("English name cannot exceed 250 characters");

            RuleFor(x => x.Code)
                .MaximumLength(250).WithMessage("Hindi name cannot exceed 250 characters");
        }
    }
}
