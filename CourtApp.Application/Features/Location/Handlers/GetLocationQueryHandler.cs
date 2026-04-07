using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Location;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Location.Handlers
{
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Result<PaginatedResult<LocationResponse>>>
    {
        private readonly ILocationRepository _repository;
        private readonly IMapper _mapper;

        public GetLocationQueryHandler(ILocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<LocationResponse>>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            var locations = await _repository.GetByStateIdAsync(request.StateId, request.PageNumber, request.PageSize);
            var totalCount = await _repository.GetCountByStateIdAsync(request.StateId);

            var mappedLocations = _mapper.Map<List<LocationResponse>>(locations);

            var paginatedResult = PaginatedResult<LocationResponse>.Success(mappedLocations, totalCount, request.PageNumber, request.PageSize);

            return Result<PaginatedResult<LocationResponse>>.Success(paginatedResult);
        }
    }
}
