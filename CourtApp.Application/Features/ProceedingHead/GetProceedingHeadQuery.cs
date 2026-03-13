using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.ProceedingHead;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class GetProceedingHeadQuery : IRequest<Result<List<GetProceedingHeadResponse>>>
    {
        
    }
    public class GetProceedingHeadQueryHandler : IRequestHandler<GetProceedingHeadQuery, Result<List<GetProceedingHeadResponse>>>
    {
        private readonly IProceedingHeadRepository repository;
        private readonly IMapper mapper;
        public GetProceedingHeadQueryHandler(IProceedingHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<List<GetProceedingHeadResponse>>> Handle(GetProceedingHeadQuery request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<List<GetProceedingHeadResponse>>(Heads);
            return Result<List<GetProceedingHeadResponse>>.Success(HeadsDt);
        }
    }


}
