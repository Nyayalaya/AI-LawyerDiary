using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class GetCourtDistrictCachedQuery : IRequest<Result<List<CourtDistrictReponse>>>
    {
        public int StateId { get; set; }
        public int DistrictId { get; set; }
    }
    public class GetCourtDistrictCachedQueryHandler : IRequestHandler<GetCourtDistrictCachedQuery, Result<List<CourtDistrictReponse>>>
    {
        private readonly ICourtDistrictCacheRepository _cacheRepository;
        private readonly IMapper _mapper;
        public GetCourtDistrictCachedQueryHandler(ICourtDistrictCacheRepository _cacheRepository, IMapper _mapper)
        {
            this._cacheRepository = _cacheRepository;
            this._mapper = _mapper;

        }
        public async Task<Result<List<CourtDistrictReponse>>> Handle(GetCourtDistrictCachedQuery request, CancellationToken cancellationToken)
        {
            var dl = await _cacheRepository.GetCachedListAsync();
            var mappedDt = _mapper.Map<List<CourtDistrictReponse>>(dl.Where(w => w.StateId == request.StateId));
            var mdt = mappedDt.Select(s => new CourtDistrictReponse
            {
                Id = s.Id,
                Name_En = s.Name_En.ToUpper(),
                StateName = s.StateName,
                Abbreviation = s.Abbreviation,
                Name_Hn = s.Name_Hn
            }).OrderBy(o => o.Name_En.ToUpper()).ToList();
            return Result<List<CourtDistrictReponse>>.Success(mdt);
        }
    }
}
