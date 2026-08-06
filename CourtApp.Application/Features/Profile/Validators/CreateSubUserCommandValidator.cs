using CourtApp.Application.Features.Profile.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Profile.Validators
{
    public class CreateSubUserCommandValidator : AbstractValidator<CreateSubUserCommand>
    {
        public CreateSubUserCommandValidator()
        {
            RuleFor(x => x.ParentUserId)
                .NotEmpty().WithMessage("Parent User ID is required")
                .NotNull().WithMessage("Parent User ID cannot be null");

            RuleFor(x => x.SubUser)
                .NotNull().WithMessage("Sub-user data is required");

            When(x => x.SubUser != null, () =>
            {
                RuleFor(x => x.SubUser.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Email must be a valid email address");

                RuleFor(x => x.SubUser.FirstName)
                    .NotEmpty().WithMessage("First name is required")
                    .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

                RuleFor(x => x.SubUser.LastName)
                    .NotEmpty().WithMessage("Last name is required")
                    .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

                RuleFor(x => x.SubUser.RelationType)
                    .NotEmpty().WithMessage("Role is required")
                    .Must(x => x.ToString().Equals("Clerk", System.StringComparison.OrdinalIgnoreCase) ||
                               x.ToString().Equals("Associate", System.StringComparison.OrdinalIgnoreCase))
                    .WithMessage("Role must be either 'Clerk' or 'Associate'");

                RuleFor(x => x.SubUser.PhoneNumber)
                    .Matches(@"^\+?[1-9]\d{1,14}$")
                    .When(x => !string.IsNullOrEmpty(x.SubUser.PhoneNumber))
                    .WithMessage("Phone number is not in a valid format");
            });
        }
    }
}
