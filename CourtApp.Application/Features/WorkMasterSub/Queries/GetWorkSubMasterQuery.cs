using CourtApp.Application.Common;
using CourtApp.Application.DTOs.WorkSub;
using MediatR;
using System;

namespace CourtApp.Application.Features.WorkMasterSub.Queries
{
    public class GetWorkSubMasterQuery : IRequest<PaginatedResult<WorkSubMasterResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid WorkId { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
