using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Interfaces.CacheRepositories.FormBuilder;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.FormBuilder
{
    public class GetFormBuilderCachedQuery : IRequest<Result<List<FormBuilderResponseDto>>>
    {
    }
    public class GetFormBuilderCachedQueryHanlder : IRequestHandler<GetFormBuilderCachedQuery, Result<List<FormBuilderResponseDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IFormBuilderCacheRepository _repository;
        public GetFormBuilderCachedQueryHanlder(IFormBuilderCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<FormBuilderResponseDto>>> Handle(GetFormBuilderCachedQuery request, CancellationToken cancellationToken)
        {
            var Dt = await _repository.GetCachedListAsync();
            var mappedDt = _mapper.Map<List<FormBuilderResponseDto>>(Dt);
            return Result<List<FormBuilderResponseDto>>.Success(mappedDt);
        }
    }
}
