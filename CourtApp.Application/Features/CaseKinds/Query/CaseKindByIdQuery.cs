using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.CaseKinds.Query
{
    public class CaseKindByIdQuery : IRequest<Result<CaseKindQueryByIdResponse>>
    {
        public Guid Id { get; set; }

        public CaseKindByIdQuery()
        {

        }
    }

    public class CaseKindByIdQueryCommandHandler : IRequestHandler<CaseKindByIdQuery, Result<CaseKindQueryByIdResponse>>
    {
        private readonly ICaseKindCacheRepository _repository;
        private readonly IMapper mapper;
        public CaseKindByIdQueryCommandHandler(ICaseKindCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this.mapper = _mapper;
        }
        public async Task<Result<CaseKindQueryByIdResponse>> Handle(CaseKindByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id);
            var mappeddata = mapper.Map<CaseKindQueryByIdResponse>(data);
            return Result<CaseKindQueryByIdResponse>.Success(mappeddata);
        }
    }
}
