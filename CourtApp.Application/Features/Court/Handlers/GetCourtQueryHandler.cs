using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Court.DTOs;
using CourtApp.Application.Features.Court.Queries;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Court.Handlers
{
    public class GetCourtQueryHandler : IRequestHandler<GetCourtQuery, Result<PaginatedResult<CourtResponse>>>
    {
        private readonly ICourtRepository _repository;
        private readonly IMapper _mapper;

        public GetCourtQueryHandler(ICourtRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<CourtResponse>>> Handle(GetCourtQuery request, CancellationToken cancellationToken)
        {
            var courts = await _repository.GetByLocationIdAsync(
                request.LocationId,
                request.PageNumber,
                request.PageSize);

            var totalCount = await _repository.GetCountByLocationIdAsync(request.LocationId);

            var mappedCourts = _mapper.Map<System.Collections.Generic.List<CourtResponse>>(courts);

            return Result<PaginatedResult<CourtResponse>>.Success(
                PaginatedResult<CourtResponse>.Success(mappedCourts, totalCount, request.PageNumber, request.PageSize));
        }
    }
}
