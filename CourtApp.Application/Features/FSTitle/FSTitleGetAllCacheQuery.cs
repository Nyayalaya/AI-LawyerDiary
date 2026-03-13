using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Constants;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.DTOs.FSTitle;
using CourtApp.Application.Features.DOType;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FSTitle
{
    public class FSTitleGetAllCacheQuery : IRequest<Result<List<FSTitleResponse>>>
    {
        public int TypeId { get; set; }
    }
    public class GetAllDOTypeCachedQueryHandler : IRequestHandler<FSTitleGetAllCacheQuery, Result<List<FSTitleResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IFSTitleCacheRepository _cachedRepo;
        public GetAllDOTypeCachedQueryHandler(IFSTitleCacheRepository _cachedRepo, IMapper _mapper)
        {
            this._cachedRepo = _cachedRepo;
            this._mapper = _mapper;
        }
        public async Task<Result<List<FSTitleResponse>>> Handle(FSTitleGetAllCacheQuery request, CancellationToken cancellationToken)
        {
            List<FSTitleEntity> DataList = new List<FSTitleEntity>();
            var Entities = await _cachedRepo.GetCachedListAsync();
            if (request.TypeId != 0)
                Entities = Entities.Where(w => w.TypeId == request.TypeId).ToList();
            var eDt = StaticDropDownDictionaries.FSType();
            var DList = from e in Entities
                        join sdt in eDt on e.TypeId equals sdt.Key
                        select new FSTitleResponse
                        {
                            Id = e.Id,
                            Name_En = e.Name_En,
                            Type = sdt.Value.ToUpper()
                        };
            var mappedProducts = _mapper.Map<List<FSTitleResponse>>(DList
                .OrderBy(o => o.Type.ToUpper())
                .ThenBy(o => o.Name_En));
            return Result<List<FSTitleResponse>>.Success(mappedProducts.ToList());
        }
    }
}
