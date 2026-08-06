using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Location;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Location.Handlers
{
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, Result<LocationByIdResponse>>
    {
        private readonly ILocationRepository _repository;
        private readonly IMapper _mapper;

        public GetLocationByIdQueryHandler(ILocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<LocationByIdResponse>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetByIdAsync(request.Id);
            if (location == null)
                return Result<LocationByIdResponse>.Fail("Location not found");

            var response = _mapper.Map<LocationByIdResponse>(location);
            return Result<LocationByIdResponse>.Success(response);
        }
    }
}
