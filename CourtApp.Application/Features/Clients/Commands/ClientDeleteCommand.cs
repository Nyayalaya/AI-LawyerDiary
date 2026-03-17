using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Clients.Commands
{
    public class ClientDeleteCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }
    
}