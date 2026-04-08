using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Domain.Enums;
using FluentValidation;
namespace CourtApp.Application.Features.Auth.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Request).NotNull();
            // 🔹 Email
            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // 🔹 Phone
            RuleFor(x => x.Request.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be a valid 10-digit number");

            // 🔹 Password
            RuleFor(x => x.Request.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            // 🔹 Confirm Password
            RuleFor(x => x.Request.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required")
                .Equal(x => x.Request.Password).WithMessage("Passwords do not match");

            // 🔹 User Type
            RuleFor(x => x.Request.UserType)
                .IsInEnum().WithMessage("Invalid user type");

            // =========================
            // 🧍 LAWYER VALIDATION
            // =========================
            When(x => x.Request.UserType == RegisterType.Lawyer, () =>
            {
                RuleFor(x => x.Request.FirstName)
                    .NotEmpty().WithMessage("First name is required");

                RuleFor(x => x.Request.LastName)
                    .NotEmpty().WithMessage("Last name is required");

                RuleFor(x => x.Request.EnrollmentNumber)
                    .NotEmpty().WithMessage("Enrollment number is required");
            });

            // =========================
            // 🏢 CORPORATE VALIDATION
            // =========================
            When(x => x.Request.UserType == RegisterType.Corporate, () =>
            {
                RuleFor(x => x.Request.CompanyName)
                    .NotEmpty().WithMessage("Company name is required");

                RuleFor(x => x.Request.RegistrationNumber)
                    .NotEmpty().WithMessage("Registration number is required");
            });
        }
    }
}