using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Extensions;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class GetProceedingHeadQuery : IRequest<PaginatedResult<GetProceedingHeadResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class GetProceedingHeadQueryHandler : IRequestHandler<GetProceedingHeadQuery, PaginatedResult<GetProceedingHeadResponse>>
    {
        private readonly IProceedingHeadRepository repository;
        private readonly IMapper mapper;
        public GetProceedingHeadQueryHandler(IProceedingHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<PaginatedResult<GetProceedingHeadResponse>> Handle(GetProceedingHeadQuery request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<List<GetProceedingHeadResponse>>(Heads);
            return Result<PaginatedResult<GetProceedingHeadResponse>>.Success(HeadsDt.ToPaginatedResult(request.PageNumber, request.PageSize));
        }
    }


}
