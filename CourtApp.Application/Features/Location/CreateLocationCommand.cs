using CourtApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Location
{
    public class CreateLocationCommand : IRequest<Result<Guid>>
    {
        public int StateId { get; set; }
        public List<LocationDetail> Locations { get; set; }

        public class LocationDetail
        {
            public string Name { get; set; }
            public int Type { get; set; }
            public Guid? ParentLocationId { get; set; }
        }
    }
}
