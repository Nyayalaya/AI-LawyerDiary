using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Cadre.Dtos;
using CourtApp.Application.Features.Cadre.Queries;
using CourtApp.Application.Features.Cadre.Services;
using MediatR;

namespace CourtApp.Application.Features.Cadre.Handlers
{
    public class GetCadreQueryHandler : IRequestHandler<GetCadreQuery, PaginatedResult<CadreResponse>>
    {
        private readonly ICadreMasterCacheRepository _cacheRepository;
        private readonly IMapper _mapper;

        public GetCadreQueryHandler(ICadreMasterCacheRepository cacheRepository, IMapper mapper)
        {
            _cacheRepository = cacheRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CadreResponse>> Handle(GetCadreQuery request, CancellationToken cancellationToken)
        {
            var cadreList = await _cacheRepository.GetCachedListAsync();
            var mappedCadres = _mapper.Map<List<CadreResponse>>(cadreList);

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                mappedCadres = mappedCadres.Where(x => 
                    x.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply pagination
            var totalCount = mappedCadres.Count;
            var paginatedCadres = mappedCadres
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return PaginatedResult<CadreResponse>.Success(paginatedCadres, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
