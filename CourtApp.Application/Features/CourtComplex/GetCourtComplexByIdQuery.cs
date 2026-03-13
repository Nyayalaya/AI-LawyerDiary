using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{

    public class GetCourtComplexByIdQuery : IRequest<Result<CourtComplexByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class GetCourtComplexByIdQueryHandler : IRequestHandler<GetCourtComplexByIdQuery, Result<CourtComplexByIdResponse>>
    {
        private readonly ICourtComplexRepository repository;
        private readonly IMapper mapper;
        public GetCourtComplexByIdQueryHandler(ICourtComplexRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<CourtComplexByIdResponse>> Handle(GetCourtComplexByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            var result = mapper.Map<CourtComplexByIdResponse>(detail);
            return Result<CourtComplexByIdResponse>.Success(result);
        }
    }
}
