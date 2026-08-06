using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMaster.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.WorkMaster.Queries
{
    public class GetWorkMasterQuery : IRequest<PaginatedResult<WorkMasterResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
