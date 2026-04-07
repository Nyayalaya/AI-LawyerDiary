using CourtApp.Application.Common;
using CourtApp.Application.Features.Cadre.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.Cadre.Queries
{
    public class GetCadreByIdQuery : IRequest<Result<CadreByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
