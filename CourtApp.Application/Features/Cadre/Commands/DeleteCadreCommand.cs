using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Cadre.Commands
{
    public class DeleteCadreCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }
}
