
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Cadre.Dtos;
using CourtApp.Application.Features.Cadre.Queries;
using CourtApp.Application.Features.Cadre.Services;
using MediatR;

namespace CourtApp.Application.Features.Cadre.Handlers
{
    public class GetCadreByIdQueryHandler : IRequestHandler<GetCadreByIdQuery, Result<CadreByIdResponse>>
    {
        private readonly ICadreMasterCacheRepository _cacheRepository;
        private readonly IMapper _mapper;

        public GetCadreByIdQueryHandler(ICadreMasterCacheRepository cacheRepository, IMapper mapper)
        {
            _cacheRepository = cacheRepository;
            _mapper = mapper;
        }

        public async Task<Result<CadreByIdResponse>> Handle(GetCadreByIdQuery request, CancellationToken cancellationToken)
        {
            var cadre = await _cacheRepository.GetByIdAsync(request.Id);
            if (cadre == null)
                return Result<CadreByIdResponse>.Fail("Cadre not found");

            var mappedCadre = _mapper.Map<CadreByIdResponse>(cadre);
            return Result<CadreByIdResponse>.Success(mappedCadre);
        }
    }
}
