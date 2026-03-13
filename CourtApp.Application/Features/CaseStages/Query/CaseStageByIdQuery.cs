using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStages.Query
{
    public class CaseStageByIdQuery : IRequest<Result<CaseStageQueryByIdResponse>>
    {
        public Guid Id { get; set; }
        public CaseStageByIdQuery()
        {

        }
    }

    public class CaseStageByIdQueryHandler : IRequestHandler<CaseStageByIdQuery, Result<CaseStageQueryByIdResponse>>
    {
        private readonly ICaseStageCacheRepository _repository;
        private readonly IMapper mapper;
        public CaseStageByIdQueryHandler(ICaseStageCacheRepository _repository, IMapper mapper)
        {
            this._repository = _repository;
            this.mapper = mapper;
        }
        public async Task<Result<CaseStageQueryByIdResponse>> Handle(CaseStageByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id);
            var mappeddata = mapper.Map<CaseStageQueryByIdResponse>(data);
            return Result<CaseStageQueryByIdResponse>.Success(mappeddata);
        }
    }
}
