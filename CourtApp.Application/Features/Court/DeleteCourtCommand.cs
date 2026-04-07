using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Court
{
    public class DeleteCourtCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }
}
