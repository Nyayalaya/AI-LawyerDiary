using CourtApp.Application.Features.Profile.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Profile.Validators
{
    public class UpdateUserOrganizationCommandValidator : AbstractValidator<UpdateUserOrganizationCommand>
    {
        public UpdateUserOrganizationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .NotNull().WithMessage("User ID cannot be null");

            RuleFor(x => x.Organization)
                .NotNull().WithMessage("Organization data is required");

            When(x => x.Organization != null, () =>
            {
                RuleFor(x => x.Organization.OrganizationId)
                    .NotEmpty().WithMessage("Organization ID is required");

                RuleFor(x => x.Organization.OrganizationName)
                    .NotEmpty().WithMessage("Organization name is required")
                    .MaximumLength(255).WithMessage("Organization name must not exceed 255 characters");

                RuleFor(x => x.Organization.Address)
                    .MaximumLength(500).WithMessage("Address must not exceed 500 characters")
                    .When(x => !string.IsNullOrEmpty(x.Organization.Address));
            });
        }
    }
}
