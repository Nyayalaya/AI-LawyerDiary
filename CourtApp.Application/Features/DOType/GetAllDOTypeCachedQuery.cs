using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Constants;
using System.Linq;
using CourtApp.Domain.Entities.LawyerDiary;
namespace CourtApp.Application.Features.DOType
{
    public class GetAllDOTypeCachedQuery : IRequest<Result<List<DOTypeResponse>>>
    {
        public int TypeId { get; set; }
    }
    public class GetAllDOTypeCachedQueryHandler : IRequestHandler<GetAllDOTypeCachedQuery, Result<List<DOTypeResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IDOTypeCacheRepository _cachedRepo;
        public GetAllDOTypeCachedQueryHandler(IDOTypeCacheRepository _cachedRepo, IMapper _mapper)
        {
            this._cachedRepo = _cachedRepo;
            this._mapper = _mapper;
        }
        public async Task<Result<List<DOTypeResponse>>> Handle(GetAllDOTypeCachedQuery request, CancellationToken cancellationToken)
        {
            List<DOTypeEntity> DataList = new List<DOTypeEntity>();
            var Entities = await _cachedRepo.GetCachedListAsync();
            if (request.TypeId != 0)
                Entities = Entities.Where(w => w.TypeId == request.TypeId).ToList();
            var eDt = StaticDropDownDictionaries.DOTypes();
            var DList = from e in Entities
                        join sdt in eDt on e.TypeId equals sdt.Key
                        select new DOTypeResponse
                        {
                            Id = e.Id,
                            Name_En = e.Name_En,
                            Type = sdt.Value
                        };
            var mappedProducts = _mapper.Map<List<DOTypeResponse>>(DList.OrderBy(o =>o.Type).ThenBy(o=>o.Name_En));
            return Result<List<DOTypeResponse>>.Success(mappedProducts);
        }
    }
}
