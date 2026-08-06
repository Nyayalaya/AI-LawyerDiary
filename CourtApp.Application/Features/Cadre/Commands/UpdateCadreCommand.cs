using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Cadre.Commands
{
    public class UpdateCadreCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
