using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.WorkSub;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class GWorkSubMstByIdQuery : IRequest<Result<WorkSubMasterByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class GWorkSubMstByIdQueryHanlder : IRequestHandler<GWorkSubMstByIdQuery, Result<WorkSubMasterByIdResponse>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IMapper mapper;
        public GWorkSubMstByIdQueryHanlder(IWorkMasterSubRepository repository, IMapper mapper)
        {
            this.repository = repository;   
            this.mapper = mapper;
        }
        public async Task<Result<WorkSubMasterByIdResponse>> Handle(GWorkSubMstByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await repository.GetByIdAsync(request.Id);
            var mappeddata = mapper.Map<WorkSubMasterByIdResponse>(data);
            return Result<WorkSubMasterByIdResponse>.Success(mappeddata);
        }
    }
}
