using FluentValidation;
using System;

namespace CourtApp.Application.Features.Location.Validators
{
    public class CreateLocationValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationValidator()
        {
            RuleFor(x => x.StateId)
                .GreaterThan(0).WithMessage("State ID is required");

            RuleFor(x => x.Locations)
                .NotEmpty().WithMessage("At least one location is required");

            RuleForEach(x => x.Locations).ChildRules(location =>
            {
                location.RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Location name is required")
                    .MaximumLength(255).WithMessage("Location name cannot exceed 255 characters");

                location.RuleFor(x => x.Type)
                    .IsInEnum().WithMessage("Invalid location type");
            });
        }
    }
}
