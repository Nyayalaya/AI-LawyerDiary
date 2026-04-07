using CourtApp.Application.Common;
using CourtApp.Application.DTOs.WorkSub;
using MediatR;
using System;

namespace CourtApp.Application.Features.WorkMasterSub.Queries
{
    public class GetWorkSubMasterByIdQuery : IRequest<Result<WorkSubMasterByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
