using CourtApp.Application.Common;
using CourtApp.Application.Features.Court.DTOs;
using CourtApp.Application.Features.Court.Queries;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Court.Handlers
{
    public class GetCourtCacheQueryHandler : IRequestHandler<GetCourtCacheQuery, Result<List<CourtResponse>>>
    {
        private readonly ICourtCacheRepository _cacheRepository;
        private readonly ICourtRepository _repository;
        private readonly AutoMapper.IMapper _mapper;

        public GetCourtCacheQueryHandler(
            ICourtCacheRepository cacheRepository,
            ICourtRepository repository,
            AutoMapper.IMapper mapper)
        {
            _cacheRepository = cacheRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<CourtResponse>>> Handle(GetCourtCacheQuery request, CancellationToken cancellationToken)
        {
            var cachedCourts = await _cacheRepository.GetByLocationAsync(request.LocationId);

            if (cachedCourts != null && cachedCourts.Any())
            {
                return Result<List<CourtResponse>>.Success(cachedCourts);
            }

            var courts = await _repository.GetByLocationIdAsync(request.LocationId, 1, int.MaxValue);
            var mappedCourts = _mapper.Map<List<CourtResponse>>(courts);

            return Result<List<CourtResponse>>.Success(mappedCourts);
        }
    }
}
