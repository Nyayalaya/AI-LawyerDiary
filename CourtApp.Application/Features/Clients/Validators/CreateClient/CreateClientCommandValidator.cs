using CourtApp.Application.Features.Clients.Commands.CreateClient;
using CourtApp.Application.Interfaces.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Validators.CreateClient
{
    public sealed class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public CreateClientCommandValidator(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Client name is required")
                .MinimumLength(2).WithMessage("Client name must be at least 2 characters")
                .MaximumLength(255).WithMessage("Client name cannot exceed 255 characters");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email format is invalid");

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Mobile number is required")
                .Matches(@"^\d{10}$").WithMessage("Mobile number must be 10 digits");

            RuleFor(x => x.Address)
                .MaximumLength(500)
                .WithMessage("Address cannot exceed 500 characters");

            RuleFor(x => x.Phone)
                .MaximumLength(20)
                .WithMessage("Phone cannot exceed 20 characters");

            RuleFor(x => x.ClientType)
                .NotEmpty()
                .WithMessage("Client type is required");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");

            // Single DB Query for uniqueness validation
            RuleFor(x => x)
                .MustAsync(BeUniqueClient)
                .WithMessage("A client with the same name, email, or mobile number already exists.");
        }

        private async Task<bool> BeUniqueClient( CreateClientCommand command,CancellationToken cancellationToken)
        {
            return !await _clientRepository.Clients
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Name.ToLower() == command.Name.ToLower()
                    && x.Email.ToLower() == command.Email.ToLower()
                    && x.Mobile == command.Mobile,
                    cancellationToken);
        }
    }
}