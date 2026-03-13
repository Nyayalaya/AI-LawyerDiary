using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster
{
    public class WorkMasterGetByIdCommand:IRequest<Result<GetWorkMasterResponse>>
    {
    }
    public class WorkMasterGetByIdCommandHandler : IRequestHandler<WorkMasterGetByIdCommand, Result<GetWorkMasterResponse>>
    {
        private readonly IWorkMasterRepository repository;
        private readonly IMapper mapper;
        public WorkMasterGetByIdCommandHandler(IWorkMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<GetWorkMasterResponse>> Handle(WorkMasterGetByIdCommand request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<GetWorkMasterResponse>(Heads);
            return Result<GetWorkMasterResponse>.Success(HeadsDt);

        }
    }
}
