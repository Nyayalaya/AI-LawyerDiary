using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CourtType.Services;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters
{
    public class GetCourtBenchQuery : IRequest<PaginatedResult<CourtBenchResponse>>
    {
        public GetCourtBenchQuery(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
        public int StateId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CourtDistrictId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetCourtBenchQueryHandler : IRequestHandler<GetCourtBenchQuery, PaginatedResult<CourtBenchResponse>>
    {
        private readonly ICourtBenchRepository _Repository;
        private readonly ICourtMasterRepository _CourtMasterRepo;
        private readonly ICourtTypeRepository _CourtTypeRepo;
        public GetCourtBenchQueryHandler(ICourtBenchRepository _Repository,
            ICourtMasterRepository _CourtMasterRepo,
            ICourtTypeRepository courtTypeRepo)
        {
            this._Repository = _Repository;
            this._CourtMasterRepo = _CourtMasterRepo;
            _CourtTypeRepo = courtTypeRepo;

        }
        public Task<PaginatedResult<CourtBenchResponse>> Handle(GetCourtBenchQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtBenchEntity, CourtBenchResponse>> expression = e => new CourtBenchResponse
            {
                Id = e.Id,
                Address = e.Address,
                CourtBench_En = e.CourtBench_En.ToUpper(),
                CourtBench_Hn = e.CourtBench_Hn
            };
            var predicate = PredicateBuilder.True<CourtBenchEntity>();
            Guid CourtMasterId = Guid.Empty;
            var CourtTypeAbb = _CourtTypeRepo
                .CourtTypeEntities
                .Where(w => w.Id.Equals(request.CourtTypeId))
                .Select(s => s.Abbreviation)
                .FirstOrDefault();
            if (CourtTypeAbb != null && CourtTypeAbb.Equals("DICT"))
            {
                CourtMasterId = _CourtMasterRepo.Entities
                .Where(w => w.StateId == request.StateId
                        && w.CourtDistrictId == request.CourtDistrictId
                        && w.CourtComplexId.Equals(request.CourtId)).Select(s => s.Id)
                .FirstOrDefault();
            }
            else
            {
                CourtMasterId = _CourtMasterRepo.Entities
                .Where(w => w.StateId == request.StateId
                        && w.CourtTypeId.Equals(request.CourtTypeId)).Select(s => s.Id)
                .FirstOrDefault();
            }
            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtMasterId == CourtMasterId);

            var dtlist = _Repository
                .Entities
                .Where(predicate)
                .OrderBy(o => o.CourtBench_En.ToUpper())
               .Select(expression).
               ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return dtlist;

        }
    }
}
