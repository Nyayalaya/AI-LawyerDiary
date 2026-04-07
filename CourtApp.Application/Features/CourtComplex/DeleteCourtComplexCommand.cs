using CourtApp.Application.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    public class DeleteCourtComplexCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
