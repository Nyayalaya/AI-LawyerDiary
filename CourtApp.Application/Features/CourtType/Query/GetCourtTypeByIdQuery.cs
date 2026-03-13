
using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtType.Query
{
    public class GetCourtTypeByIdQuery : IRequest<Result<GetCourtTypeResponse>>
    {
        public Guid Id { get; set; }
    }
}
