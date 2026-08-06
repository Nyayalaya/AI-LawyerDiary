using CourtApp.Application.Features.Profile.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Profile.Validators
{
    public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
    {
        public CreateOrganizationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .NotNull().WithMessage("User ID cannot be null");

            RuleFor(x => x.Organization)
                .NotNull().WithMessage("Organization data is required");

            When(x => x.Organization != null, () =>
            {
                RuleFor(x => x.Organization.OrganizationName)
                    .NotEmpty().WithMessage("Organization name is required")
                    .MaximumLength(255).WithMessage("Organization name must not exceed 255 characters");

                RuleFor(x => x.Organization.RegistrationNumber)
                    .NotEmpty().WithMessage("Registration number is required")
                    .MaximumLength(100).WithMessage("Registration number must not exceed 100 characters");

                RuleFor(x => x.Organization.TaxIdentificationNumber)
                    .MaximumLength(50).WithMessage("Tax identification number must not exceed 50 characters")
                    .When(x => !string.IsNullOrEmpty(x.Organization.TaxIdentificationNumber));
            });
        }
    }
}
