using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseKinds.Query
{
    public class CaseKindAllCacheQuery : IRequest<Result<List<CaseKindCacheQueryResponse>>>
    {
        public Guid CourtTypeId { get; set; }
    }

    public class CaseKindAllCacheQueryCommandHandler : IRequestHandler<CaseKindAllCacheQuery, Result<List<CaseKindCacheQueryResponse>>>
    {
        private readonly ICaseKindRepository _repository;
        private readonly IMapper _mapper;

        public CaseKindAllCacheQueryCommandHandler(ICaseKindRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public Task<Result<List<CaseKindCacheQueryResponse>>> Handle(CaseKindAllCacheQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseKindEntity, CaseKindCacheQueryResponse>> expression = e => new CaseKindCacheQueryResponse
            {
                Id = e.Id,
                CourtType = e.CourtType.Name,
                CaseKind = e.CaseKind
            };
            var predicate = PredicateBuilder.True<CaseKindEntity>();

            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtType.Id == request.CourtTypeId);

            var data = _repository.QryEntities.Where(predicate).Select(expression).ToList();
            var mappeddata = _mapper.Map<List<CaseKindCacheQueryResponse>>(data);
            return Task.FromResult(Result<List<CaseKindCacheQueryResponse>>.Success(mappeddata));
        }
    }
}
