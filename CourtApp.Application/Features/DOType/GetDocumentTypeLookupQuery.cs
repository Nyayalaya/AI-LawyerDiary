using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DOType
{
    public class GetDocumentTypeLookupQuery:IRequest<Result<List<DdlGuidStringDto>>>
    {
        public int TypeId { get; set; }
    }

    public class GetDocumentTypeLookupQueryHandler : IRequestHandler<GetDocumentTypeLookupQuery, Result<List<DdlGuidStringDto>>>
    {
        private readonly IDOTypeCacheRepository _cachedRepo;
        public GetDocumentTypeLookupQueryHandler(IDOTypeCacheRepository cachedRepo)
        {
            _cachedRepo = cachedRepo;
        }
        public async Task<Result<List<DdlGuidStringDto>>> Handle(GetDocumentTypeLookupQuery request, CancellationToken cancellationToken)
        {
            var documentTypes = await _cachedRepo.GetCachedListAsync();
            var result = documentTypes.Where(dt => dt.TypeId == request.TypeId).Select(dt => new DdlGuidStringDto
            {
                Id = dt.Id,
                Name = dt.Name_En
            }).ToList();
            return Result<List<DdlGuidStringDto>>.Success(result);
        }
    }   
}
