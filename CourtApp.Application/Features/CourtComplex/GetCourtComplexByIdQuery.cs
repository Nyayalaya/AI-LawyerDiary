using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtComplex;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    public class GetCourtComplexByIdQuery : IRequest<Result<CourtComplexByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
