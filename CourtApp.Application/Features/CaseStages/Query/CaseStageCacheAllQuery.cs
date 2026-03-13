using System;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace CourtApp.Application.Features.CaseStages.Query
{
    public class CaseStageCacheAllQuery : IRequest<Result<List<CaseStageCacheAllQueryResponse>>>
    {
        public CaseStageCacheAllQuery()
        {

        }
    }

    public class CaseStageCacheAllQueryHanlder : IRequestHandler<CaseStageCacheAllQuery, Result<List<CaseStageCacheAllQueryResponse>>>
    {
        private readonly ICaseStageCacheRepository _repository;
        private readonly IMapper _mapper;
        public CaseStageCacheAllQueryHanlder(ICaseStageCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<CaseStageCacheAllQueryResponse>>> Handle(CaseStageCacheAllQuery request, CancellationToken cancellationToken)
        {
            var dt = await _repository.GetCachedListAsync();
            var mappeddata = _mapper.Map<List<CaseStageCacheAllQueryResponse>>(dt);
            var mdt = mappeddata.Select(s => new CaseStageCacheAllQueryResponse
            {
                Id = s.Id,
                CaseStage = s.CaseStage.ToUpper()
            }).OrderBy(o => o.CaseStage.ToUpper());
            return Result<List<CaseStageCacheAllQueryResponse>>.Success(mdt.ToList());
        }
    }
}
