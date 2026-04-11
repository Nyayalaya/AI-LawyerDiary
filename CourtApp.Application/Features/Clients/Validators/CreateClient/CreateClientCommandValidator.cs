using FluentValidation;
using CourtApp.Application.Features.Clients.Commands.CreateClient;

namespace CourtApp.Application.Features.Clients.Validators.CreateClient
{
    public sealed class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Client name is required")
                .MinimumLength(2).WithMessage("Client name must be at least 2 characters")
                .MaximumLength(255).WithMessage("Client name cannot exceed 255 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email format is invalid");

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("Mobile number is required")
                .Matches(@"^\d{10}$").WithMessage("Mobile number must be 10 digits");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters");

            RuleFor(x => x.ClientType)
                .NotEmpty().WithMessage("Client type is required");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");
        }
    }
}
