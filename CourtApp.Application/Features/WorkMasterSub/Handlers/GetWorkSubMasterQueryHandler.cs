using CourtApp.Application.Common;
using CourtApp.Application.DTOs.WorkSub;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.WorkMasterSub.Queries;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Handlers
{
    public class GetWorkSubMasterQueryHandler : IRequestHandler<GetWorkSubMasterQuery, PaginatedResult<WorkSubMasterResponse>>
    {
        private readonly IWorkMasterSubRepository _repository;

        public GetWorkSubMasterQueryHandler(IWorkMasterSubRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<WorkSubMasterResponse>> Handle(GetWorkSubMasterQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<WorksEntity, WorkSubMasterResponse>> expression = e => new WorkSubMasterResponse
            {
                Id = e.Id,
                WorkName = e.Work.Name,
                Name_En = e.Name,
                Abbreviation = e.Code
            };

            var predicate = PredicateBuilder.True<WorksEntity>();
            if (request.WorkId != Guid.Empty)
                predicate = predicate.And(b => b.WorkId == request.WorkId);
            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtTypeId == request.CourtTypeId);

            var paginatedList = await _repository.Entities
                .Include(w => w.Work)
                .Where(predicate)
                .Select(expression)
                .ToListAsync(cancellationToken);

            return paginatedList.ToPaginatedResult(request.PageNumber, request.PageSize);
        }
    }
}
