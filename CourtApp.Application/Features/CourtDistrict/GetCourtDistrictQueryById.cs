using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class GetCourtDistrictQueryById:IRequest<Result<CourtDistrictByIdReponse>>
    {
        public Guid Id { get; set; }
    }
    public class GetCourtDistrictQueryByIdHandler : IRequestHandler<GetCourtDistrictQueryById, Result<CourtDistrictByIdReponse>>
    {
        private readonly ICourtDistrictRepository repository;
        private readonly IMapper mapper;
        public GetCourtDistrictQueryByIdHandler(ICourtDistrictRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper= mapper;
        }
        public async Task<Result<CourtDistrictByIdReponse>> Handle(GetCourtDistrictQueryById request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            var result = mapper.Map<CourtDistrictByIdReponse>(detail);
            return Result<CourtDistrictByIdReponse>.Success(result);
        }
    }
}
