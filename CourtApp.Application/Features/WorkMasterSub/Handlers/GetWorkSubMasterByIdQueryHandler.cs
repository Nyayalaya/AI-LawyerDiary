using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.DTOs.WorkSub;
using CourtApp.Application.Features.WorkMasterSub.Queries;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Handlers
{
    public class GetWorkSubMasterByIdQueryHandler : IRequestHandler<GetWorkSubMasterByIdQuery, Result<WorkSubMasterByIdResponse>>
    {
        private readonly IWorkMasterSubRepository _repository;
        private readonly IMapper _mapper;

        public GetWorkSubMasterByIdQueryHandler(IWorkMasterSubRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<WorkSubMasterByIdResponse>> Handle(GetWorkSubMasterByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id);
            if (data == null)
                return Result<WorkSubMasterByIdResponse>.Fail("Record not found.");

            var mappedData = _mapper.Map<WorkSubMasterByIdResponse>(data);
            return Result<WorkSubMasterByIdResponse>.Success(mappedData);
        }
    }
}
