

using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtType.Command
{
    public class DeleteCourtTypeCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }

    
}
