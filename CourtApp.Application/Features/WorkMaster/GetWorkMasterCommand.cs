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
    public class GetWorkMasterCommand : IRequest<Result<List<GetWorkMasterResponse>>>
    {
        public Guid Id { get; set; }
    }
    public class GetWorkMasterCommandHandler : IRequestHandler<GetWorkMasterCommand, Result<List<GetWorkMasterResponse>>>
    {
        private readonly IWorkMasterRepository repository;
        private readonly IMapper mapper;
        public GetWorkMasterCommandHandler(IWorkMasterRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<List<GetWorkMasterResponse>>> Handle(GetWorkMasterCommand request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<List<GetWorkMasterResponse>>(Heads);
            return Result<List<GetWorkMasterResponse>>.Success(HeadsDt);

        }
    }
}
