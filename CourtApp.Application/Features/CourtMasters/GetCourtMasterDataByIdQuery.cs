using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Interfaces.Repositories;
using System;
using CourtApp.Application.DTOs.CourtMaster;

namespace CourtApp.Application.Features.CourtMasters
{
    public class GetCourtMasterDataByIdQuery : IRequest<Result<GetCourtMasterDataByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetCourtMasterDataByIdQueryHandler : IRequestHandler<GetCourtMasterDataByIdQuery, Result<GetCourtMasterDataByIdResponse>>
    {
        private readonly ICourtMasterRepository _repository;
        private readonly IMapper mapper;
        public GetCourtMasterDataByIdQueryHandler(ICourtMasterRepository _repository, IMapper mapper)
        {
            this.mapper = mapper;
            this._repository = _repository;
        }

        public async Task<Result<GetCourtMasterDataByIdResponse>> Handle(GetCourtMasterDataByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id);
            var mappeddata = mapper.Map<GetCourtMasterDataByIdResponse>(data);
            mappeddata.IsHighCourt = mappeddata.CourtDistrictId != Guid.Empty ? false : true;
            return Result<GetCourtMasterDataByIdResponse>.Success(mappeddata);
        }
    }
}
