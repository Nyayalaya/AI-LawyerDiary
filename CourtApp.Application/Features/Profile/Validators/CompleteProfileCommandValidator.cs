using CourtApp.Application.Features.Profile.Commands;
using FluentValidation;
using System;

namespace CourtApp.Application.Features.Profile.Validators
{
    public class CompleteProfileCommandValidator : AbstractValidator<CompleteProfileCommand>
    {
        public CompleteProfileCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .NotNull().WithMessage("User ID cannot be null");

            RuleFor(x => x.Profile)
                .NotNull().WithMessage("Profile data is required");

            When(x => x.Profile != null, () =>
            {
                RuleFor(x => x.Profile.FirstName)
                    .NotEmpty().WithMessage("First name is required")
                    .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

                RuleFor(x => x.Profile.LastName)
                    .NotEmpty().WithMessage("Last name is required")
                    .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

                RuleFor(x => x.Profile.DateOfBirth)
                    .NotEmpty().WithMessage("Date of birth is required")
                    .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past");

                RuleFor(x => x.Profile.Gender)
                    .IsInEnum().WithMessage("Gender must be a valid value");

                RuleFor(x => x.Profile.ProfileImageUrl)
                    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                    .When(x => !string.IsNullOrEmpty(x.Profile.ProfileImageUrl))
                    .WithMessage("Profile image URL is not a valid URI");

                RuleFor(x => x.Profile.Addresses)
                    .NotNull().WithMessage("Addresses list is required");

                RuleFor(x => x.Profile.Contacts)
                    .NotNull().WithMessage("Contacts list is required");
            });
        }
    }
}
