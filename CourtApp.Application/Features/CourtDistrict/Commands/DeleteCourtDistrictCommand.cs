using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtDistrict.Commands
{
    public class DeleteCourtDistrictCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
