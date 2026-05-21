using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Domain.Enums;
using FluentValidation;
namespace CourtApp.Application.Features.Auth.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {

            RuleFor(x => x)
                .NotNull()
                .WithMessage("Registration data is required");
           
            // 🔹 Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // 🔹 Phone
            RuleFor(x => x.Contact)
                .NotEmpty().WithMessage("Contact number is required")
                .Matches(@"^\d{10}$").WithMessage("Contact number must be a valid 10-digit number");

            // 🔹 Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            // 🔹 Confirm Password
            //RuleFor(x => x.ConfirmPassword)
            //    .NotEmpty().WithMessage("Confirm password is required")
            //    .Equal(x => x.Password).WithMessage("Passwords do not match");

            // 🔹 User Type
            RuleFor(x => x.UserType)
                .IsInEnum().WithMessage("Invalid user type");

            // =========================
            // 🧍 LAWYER VALIDATION
            // =========================
            When(x => x.UserType == RegisterType.Lawyer, () =>
            {
                RuleFor(x => x.IndividualInfoDto.FirstName)
                    .NotEmpty().WithMessage("First name is required");

                RuleFor(x => x.IndividualInfoDto.LastName)
                    .NotEmpty().WithMessage("Last name is required");

                RuleFor(x => x.IndividualInfoDto.EnrollmentNumber)
                    .NotEmpty().WithMessage("Enrollment number is required");
            });

            // =========================
            // 🏢 CORPORATE VALIDATION
            // =========================
            When(x => x.UserType == RegisterType.Corporate, () =>
            {
                RuleFor(x => x.CompanyInfoDto.CompanyName)
                    .NotEmpty().WithMessage("Company name is required");

                RuleFor(x => x.CompanyInfoDto.RegistrationNumber)
                    .NotEmpty().WithMessage("Registration number is required");
            });
        }
    }
}