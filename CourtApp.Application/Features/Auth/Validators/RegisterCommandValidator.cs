using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Domain.Enums;
using FluentValidation;
namespace CourtApp.Application.Features.Auth.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {

            RuleFor(x => x.RegistrationRequestData)
                .NotNull()
                .WithMessage("Registration data is required");
           
            // 🔹 Email
            RuleFor(x => x.RegistrationRequestData.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // 🔹 Phone
            RuleFor(x => x.RegistrationRequestData.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be a valid 10-digit number");

            // 🔹 Password
            RuleFor(x => x.RegistrationRequestData.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            // 🔹 Confirm Password
            RuleFor(x => x.RegistrationRequestData.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required")
                .Equal(x => x.RegistrationRequestData.Password).WithMessage("Passwords do not match");

            // 🔹 User Type
            RuleFor(x => x.RegistrationRequestData.UserType)
                .IsInEnum().WithMessage("Invalid user type");

            // =========================
            // 🧍 LAWYER VALIDATION
            // =========================
            When(x => x.RegistrationRequestData.UserType == RegisterType.Lawyer, () =>
            {
                RuleFor(x => x.RegistrationRequestData.FirstName)
                    .NotEmpty().WithMessage("First name is required");

                RuleFor(x => x.RegistrationRequestData.LastName)
                    .NotEmpty().WithMessage("Last name is required");

                RuleFor(x => x.RegistrationRequestData.EnrollmentNumber)
                    .NotEmpty().WithMessage("Enrollment number is required");
            });

            // =========================
            // 🏢 CORPORATE VALIDATION
            // =========================
            When(x => x.RegistrationRequestData.UserType == RegisterType.Corporate, () =>
            {
                RuleFor(x => x.RegistrationRequestData.CompanyName)
                    .NotEmpty().WithMessage("Company name is required");

                RuleFor(x => x.RegistrationRequestData.RegistrationNumber)
                    .NotEmpty().WithMessage("Registration number is required");
            });
        }
    }
}