using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseStage.Services;
using CourtApp.Application.Features.CaseStages.Query;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Handlers
{
    public class CaseStageByIdQueryHandler : IRequestHandler<CaseStageByIdQuery, Result<CaseStageQueryByIdResponse>>
    {
        private readonly ICaseStageRepository _repository;
        private readonly IMapper mapper;
        public CaseStageByIdQueryHandler(ICaseStageRepository _repository, IMapper mapper)
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
