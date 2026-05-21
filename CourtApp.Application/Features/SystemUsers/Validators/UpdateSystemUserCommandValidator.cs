using CourtApp.Application.Features.SystemUsers.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.SystemUsers.Validators
{
    /// <summary>
    /// Validator for UpdateSystemUserCommand
    /// </summary>
    public class UpdateSystemUserCommandValidator : AbstractValidator<UpdateSystemUserCommand>
    {
        public UpdateSystemUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Request)
                .NotNull().WithMessage("Request cannot be null.");

            RuleFor(x => x.Request.StatusReason)
                .MaximumLength(500).WithMessage("Status reason cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Request.StatusReason));
        }
    }
}
