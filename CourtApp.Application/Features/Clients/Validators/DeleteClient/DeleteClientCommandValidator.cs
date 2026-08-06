using System;
using FluentValidation;
using CourtApp.Application.Features.Clients.Commands.DeleteClient;

namespace CourtApp.Application.Features.Clients.Validators.DeleteClient
{
    public sealed class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Client ID is required");
        }
    }
}
