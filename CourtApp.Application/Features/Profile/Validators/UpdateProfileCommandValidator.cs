using CourtApp.Application.Features.Profile.Commands;
using FluentValidation;
using System;

namespace CourtApp.Application.Features.Profile.Validators
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.Profile)
                .NotNull().WithMessage("Profile data is required");

            When(x => x.Profile != null, () =>
            {
                RuleFor(x => x.Profile.UserId)
                    .NotEmpty().WithMessage("User ID is required");

                RuleFor(x => x.Profile.FirstName)
                    .NotEmpty().WithMessage("First name is required")
                    .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

                RuleFor(x => x.Profile.LastName)
                    .NotEmpty().WithMessage("Last name is required")
                    .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

                RuleFor(x => x.Profile.PhoneNumber)
                    .Matches(@"^\+?[1-9]\d{1,14}$")
                    .When(x => !string.IsNullOrEmpty(x.Profile.PhoneNumber))
                    .WithMessage("Phone number is not in a valid format");

                RuleFor(x => x.Profile.ProfileImageUrl)
                    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                    .When(x => !string.IsNullOrEmpty(x.Profile.ProfileImageUrl))
                    .WithMessage("Profile image URL is not a valid URI");
            });
        }
    }
}
