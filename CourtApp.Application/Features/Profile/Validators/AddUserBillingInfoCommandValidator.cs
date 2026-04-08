using CourtApp.Application.Features.Profile.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Profile.Validators
{
    public class AddUserBillingInfoCommandValidator : AbstractValidator<AddUserBillingInfoCommand>
    {
        public AddUserBillingInfoCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .NotNull().WithMessage("User ID cannot be null");

            RuleFor(x => x.BillingInfo)
                .NotNull().WithMessage("Billing information is required");

            When(x => x.BillingInfo != null, () =>
            {
                RuleFor(x => x.BillingInfo.BillingName)
                    .NotEmpty().WithMessage("Billing name is required")
                    .MaximumLength(255).WithMessage("Billing name must not exceed 255 characters");

                RuleFor(x => x.BillingInfo.BillingAddress)
                    .NotEmpty().WithMessage("Billing address is required")
                    .MaximumLength(500).WithMessage("Billing address must not exceed 500 characters");

                RuleFor(x => x.BillingInfo.City)
                    .NotEmpty().WithMessage("City is required")
                    .MaximumLength(100).WithMessage("City must not exceed 100 characters");

                RuleFor(x => x.BillingInfo.PostalCode)
                    .NotEmpty().WithMessage("Postal code is required")
                    .MaximumLength(20).WithMessage("Postal code must not exceed 20 characters");

                RuleFor(x => x.BillingInfo.State)
                    .NotEmpty().WithMessage("State is required")
                    .MaximumLength(100).WithMessage("State must not exceed 100 characters");

                RuleFor(x => x.BillingInfo.Country)
                    .NotEmpty().WithMessage("Country is required")
                    .MaximumLength(100).WithMessage("Country must not exceed 100 characters");
            });
        }
    }
}
