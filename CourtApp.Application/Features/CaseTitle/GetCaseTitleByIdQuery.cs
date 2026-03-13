using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.CaseTitleRes;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseTitle
{

    public class GetCaseTitleByIdQuery : IRequest<Result<CaseTitleByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class GetCaseTitleByIdQueryHandler : IRequestHandler<GetCaseTitleByIdQuery, Result<CaseTitleByIdResponse>>
    {
        private readonly ICaseTitleRepository repository;
        private readonly IMapper mapper;
        public GetCaseTitleByIdQueryHandler(ICaseTitleRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<CaseTitleByIdResponse>> Handle(GetCaseTitleByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            var result = mapper.Map<CaseTitleByIdResponse>(detail);
            return Result<CaseTitleByIdResponse>.Success(result);
        }
    }
}
