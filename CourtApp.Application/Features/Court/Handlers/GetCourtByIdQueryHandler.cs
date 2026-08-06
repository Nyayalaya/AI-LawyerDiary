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
    public class GetCourtByIdQueryHandler : IRequestHandler<GetCourtByIdQuery, Result<CourtByIdResponse>>
    {
        private readonly ICourtRepository _repository;
        private readonly IMapper _mapper;

        public GetCourtByIdQueryHandler(ICourtRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CourtByIdResponse>> Handle(GetCourtByIdQuery request, CancellationToken cancellationToken)
        {
            var court = await _repository.GetByIdAsync(request.Id);
            if (court == null)
                return Result<CourtByIdResponse>.Fail($"Court with ID '{request.Id}' not found");

            var response = _mapper.Map<CourtByIdResponse>(court);

            return Result<CourtByIdResponse>.Success(response);
        }
    }
}
