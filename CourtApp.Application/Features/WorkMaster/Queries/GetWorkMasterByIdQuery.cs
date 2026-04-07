using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMaster.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.WorkMaster.Queries
{
    public class GetWorkMasterByIdQuery : IRequest<Result<WorkMasterByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
