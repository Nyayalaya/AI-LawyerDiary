using CourtApp.Application.Features.CaseDetails.Commands;
using FluentValidation;
using System;

namespace CourtApp.Application.Features.CaseDetails.Validators
{
    public class CaseAssignedCommandValidator : AbstractValidator<CaseAssignedCommand>
    {
        public CaseAssignedCommandValidator()
        {
            RuleFor(x => x.CaseId)
                .NotEmpty()
                .WithMessage("CaseId is required.");

            RuleFor(x => x.LawyerId)
                .NotEmpty()
                .Must(BeValidGuid)
                .WithMessage("LawyerId must be a valid GUID.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");
        }

        private static bool BeValidGuid(string value)
            => Guid.TryParse(value, out _);
    }
}
