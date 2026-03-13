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
    public class GetProceedingHeadByIdQuery : IRequest<Result<GetProceedingHeadResponse>>
    {
        public  Guid Id { get; set; }
    }
    public class GetProceedingHeadByIdQueryHandler : IRequestHandler<GetProceedingHeadByIdQuery, Result<GetProceedingHeadResponse>>
    {
        private readonly IProceedingHeadRepository repository;
        private readonly IMapper mapper;
        public GetProceedingHeadByIdQueryHandler(IProceedingHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<GetProceedingHeadResponse>> Handle(GetProceedingHeadByIdQuery request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetByIdAsync(request.Id);
            var HeadsDt = mapper.Map<GetProceedingHeadResponse>(Heads);
            return Result<GetProceedingHeadResponse>.Success(HeadsDt);

        }
    }


}
