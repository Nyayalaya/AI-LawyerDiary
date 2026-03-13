using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.ProcSubHead;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class GetProceedingSubHeadGetByIdQuery : IRequest<Result<GetProcSubHeadByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class GetProceedingSubHeadGetByIdQueryHandler : IRequestHandler<GetProceedingSubHeadGetByIdQuery, Result<GetProcSubHeadByIdResponse>>
    {
       
        private readonly IProceedingSubHeadRepository _repository;
        private IMapper _Mapper { get; set; }
        public GetProceedingSubHeadGetByIdQueryHandler(IMapper _Mapper, IProceedingSubHeadRepository repository)
        {
            this._Mapper = _Mapper;
            _repository = repository;

        }
        public async Task<Result<GetProcSubHeadByIdResponse>> Handle(GetProceedingSubHeadGetByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);
            var result = _Mapper.Map<GetProcSubHeadByIdResponse>(detail);
            return Result<GetProcSubHeadByIdResponse>.Success(result);
        }
    }
}
