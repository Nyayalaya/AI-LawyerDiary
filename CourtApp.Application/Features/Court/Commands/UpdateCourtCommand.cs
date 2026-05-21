using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Court.Commands
{
    public class UpdateCourtCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtLevelId { get; set; }
    }
}
