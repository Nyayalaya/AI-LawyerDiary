// CourtApp.Application/Features/CourtType/Validators/CreateCourtTypeCommandValidator.cs
using CourtApp.Application.Features.CourtType.Command;
using FluentValidation;

namespace CourtApp.Application.Features.CourtType.Validators
{
    public sealed class CreateCourtTypeCommandValidator
        : AbstractValidator<CreateCourtTypeCommand>
    {
        // Allows letters, numbers, spaces, hyphens, dots only — no special chars
        private const string SafeTextPattern = @"^[a-zA-Z0-9\u0600-\u06FF\s\-\.]+$";

        public CreateCourtTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Court type name is required.")
                .MinimumLength(2)
                    .WithMessage("Court type name must be at least 2 characters.")
                .MaximumLength(100)
                    .WithMessage("Court type name must not exceed 100 characters.")
                .Matches(SafeTextPattern)
                    .WithMessage("Court type name must not contain special characters.");

            RuleFor(x => x.Code)
                .NotEmpty()
                    .WithMessage("Abbreviation is required.")
                .MinimumLength(1)
                    .WithMessage("Abbreviation must be at least 1 character.")
                .MaximumLength(10)
                    .WithMessage("Abbreviation must not exceed 10 characters.")
                .Matches(@"^[a-zA-Z0-9]+$")
                    .WithMessage("Abbreviation must contain letters and numbers only — no spaces or special characters.");
        }
    }
}