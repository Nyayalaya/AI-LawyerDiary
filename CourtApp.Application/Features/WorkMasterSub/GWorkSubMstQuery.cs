using CourtApp.Application.Common;
using CourtApp.Application.DTOs.WorkSub;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class GWorkSubMstQuery : IRequest<PaginatedResult<WorkSubMasterResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid WorkId { get; set; }
    }
    public class GWorkSubMstQueryHandler : IRequestHandler<GWorkSubMstQuery, PaginatedResult<WorkSubMasterResponse>>
    {
        private readonly IWorkMasterSubRepository repository;
        public GWorkSubMstQueryHandler(IWorkMasterSubRepository repository)
        {
            this.repository = repository;
        }
        public async Task<PaginatedResult<WorkSubMasterResponse>> Handle(GWorkSubMstQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<WorkMasterSubEntity, WorkSubMasterResponse>> expression = e => new WorkSubMasterResponse
            {
                Id = e.Id,
                WorkName = e.Work.Work_En,
                Abbreviation = e.Abbreviation,
                Name_En = e.Name_En,
                Name_Hn = e.Name_Hn
            };
            var predicate = PredicateBuilder.True<WorkMasterSubEntity>();
            if (request.WorkId != Guid.Empty)
                predicate = predicate.And(b => b.WorkId == request.WorkId);

            var paginatedList = await repository.Entities
                .Include(w=>w.Work)
                .Where(predicate)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}
