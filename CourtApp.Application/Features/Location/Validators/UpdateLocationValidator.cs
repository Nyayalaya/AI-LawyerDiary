using FluentValidation;
using System;

namespace CourtApp.Application.Features.Location.Validators
{
    public class UpdateLocationValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Location ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Location name is required")
                .MaximumLength(255).WithMessage("Location name cannot exceed 255 characters");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid location type");
        }
    }
}
