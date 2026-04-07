using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Location
{
    public class UpdateLocationCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public Guid? ParentLocationId { get; set; }
    }
}
