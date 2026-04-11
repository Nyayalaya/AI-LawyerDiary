using System;
using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.Clients.Commands.DeleteClient
{
    public sealed class DeleteClientCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
