using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.Cadre;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre
{
    public class GetQueryByIdCadre : IRequest<Result<GetCadreResponseById>>
    {
        public Guid Id { get; set; }
    }
    public class GetQueryByIdCadreHandler : IRequestHandler<GetQueryByIdCadre, Result<GetCadreResponseById>>
    {
        private readonly ICadreMasterCacheRepository _cacheRepository;
        private IMapper mapper;
        public GetQueryByIdCadreHandler(ICadreMasterCacheRepository _cacheRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this._cacheRepository = _cacheRepository;
        }
        public async Task<Result<GetCadreResponseById>> Handle(GetQueryByIdCadre request, CancellationToken cancellationToken)
        {
            var detail = await _cacheRepository.GetByIdAsync(request.Id);
            var mapedDetail = mapper.Map<GetCadreResponseById>(detail);
            return Result<GetCadreResponseById>.Success(mapedDetail);
        }
    }
}
