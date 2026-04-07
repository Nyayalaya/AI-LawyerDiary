using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMaster.Dtos;
using CourtApp.Application.Features.WorkMaster.Queries;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Handlers
{
    public class GetWorkMasterByIdQueryHandler : IRequestHandler<GetWorkMasterByIdQuery, Result<WorkMasterByIdResponse>>
    {
        private readonly IWorkMasterRepository _repository;
        private readonly IMapper _mapper;

        public GetWorkMasterByIdQueryHandler(IWorkMasterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<WorkMasterByIdResponse>> Handle(GetWorkMasterByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return Result<WorkMasterByIdResponse>.Fail("Work Master not found.");

            var response = _mapper.Map<WorkMasterByIdResponse>(entity);
            return Result<WorkMasterByIdResponse>.Success(response);
        }
    }
}
