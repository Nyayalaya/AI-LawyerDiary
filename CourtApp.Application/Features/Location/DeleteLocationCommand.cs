using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Location
{
    public class DeleteLocationCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }
}
