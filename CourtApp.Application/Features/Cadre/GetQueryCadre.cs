using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.Cadre;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre
{
    public class GetQueryCadre:IRequest<Result<List<GetCadreResponse>>>
    {
    }
    public class GetQueryCadreHandler : IRequestHandler<GetQueryCadre, Result<List<GetCadreResponse>>>
    {
        private readonly ICadreMasterCacheRepository _cacheRepository;
        private IMapper mapper;
        public GetQueryCadreHandler(ICadreMasterCacheRepository _cacheRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this._cacheRepository= _cacheRepository;
        }
        public async Task<Result<List<GetCadreResponse>>> Handle(GetQueryCadre request, CancellationToken cancellationToken)
        {
            var bookTypeList = await _cacheRepository.GetCachedListAsync();
            var mappedBookTye = mapper.Map<List<GetCadreResponse>>(bookTypeList);
            return Result<List<GetCadreResponse>>.Success(mappedBookTye);
        }
    }
}
