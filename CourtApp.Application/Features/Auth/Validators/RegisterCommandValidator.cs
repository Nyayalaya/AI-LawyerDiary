using FluentValidation;

using CourtApp.Domain.Enums;
using System;
using CourtApp.Application.Features.Auth.Commands;

namespace CourtApp.Application.Features.Auth.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            // Email validation
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email format is invalid");

            // Password validation
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit")
                .Matches(@"[!@#$%^&*]").WithMessage("Password must contain at least one special character");

            // Contact validation
            RuleFor(x => x.Contact)
                .NotEmpty().WithMessage("Contact number is required")
                .Matches(@"^\d{10}$").WithMessage("Contact must be a valid 10-digit number");

            // User Type validation
            RuleFor(x => x.UserType)
                .IsInEnum().WithMessage("Invalid User Type");

            // Individual Info validation
            RuleFor(x => x.IndividualInfoDto)
                .NotNull().WithMessage("Individual information is required")
                .When(x => x.UserType == RegisterType.Lawyer || x.UserType == RegisterType.Client);

            RuleFor(x => x.IndividualInfoDto.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .MaximumLength(100).WithMessage("First Name cannot exceed 100 characters")
                .When(x => x.IndividualInfoDto != null);

            RuleFor(x => x.IndividualInfoDto.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(100).WithMessage("Last Name cannot exceed 100 characters")
                .When(x => x.IndividualInfoDto != null);

            RuleFor(x => x.IndividualInfoDto.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required")
                .Must(dob => dob < DateTime.Now.AddYears(-18)).WithMessage("User must be at least 18 years old")
                .When(x => x.IndividualInfoDto != null);

            RuleFor(x => x.IndividualInfoDto.Gender)
                .IsInEnum().WithMessage("Invalid Gender")
                .When(x => x.IndividualInfoDto != null);

            RuleFor(x => x.IndividualInfoDto.EnrollmentNumber)
                .NotEmpty().WithMessage("Enrollment Number is required")
                .When(x => x.UserType == RegisterType.Lawyer && x.IndividualInfoDto != null);

            // Company Info validation
            RuleFor(x => x.CompanyInfoDto)
                .NotNull().WithMessage("Company information is required")
                .When(x => x.UserType == RegisterType.Corporate);

            RuleFor(x => x.CompanyInfoDto.CompanyName)
                .NotEmpty().WithMessage("Company Name is required")
                .MaximumLength(200).WithMessage("Company Name cannot exceed 200 characters")
                .When(x => x.CompanyInfoDto != null);

            RuleFor(x => x.CompanyInfoDto.RegistrationNumber)
                .NotEmpty().WithMessage("Registration Number is required")
                .When(x => x.CompanyInfoDto != null);

            RuleFor(x => x.CompanyInfoDto.AutherizedPerson)
                .NotEmpty().WithMessage("Authorized Person name is required")
                .When(x => x.CompanyInfoDto != null);
        }
    }
}