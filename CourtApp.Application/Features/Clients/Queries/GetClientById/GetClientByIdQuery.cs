using System;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Clients.Queries.GetClientById
{
    public sealed class GetClientByIdQuery : IRequest<Result<ClientResponseDto>>
    {
        public Guid Id { get; set; }
    }
}
