using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookTypes.Query.GetAllCached
{
    public class GetAllBookTypeCachedQuery : IRequest<Result<List<GetAllBookTypeCachedResponse>>>
    {
        public GetAllBookTypeCachedQuery()
        {
        }
    }

    public class GetAllBrandsCachedQueryHandler : IRequestHandler<GetAllBookTypeCachedQuery, Result<List<GetAllBookTypeCachedResponse>>>
    {
        private readonly IBookTypeCacheRepository _bookTypeCache;
        private readonly IMapper _mapper;

        public GetAllBrandsCachedQueryHandler(IBookTypeCacheRepository _bookTypeCache, IMapper mapper)
        {
            this._bookTypeCache = _bookTypeCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllBookTypeCachedResponse>>> Handle(GetAllBookTypeCachedQuery request, CancellationToken cancellationToken)
        {
            var bookTypeList = await _bookTypeCache.GetCachedListAsync();
            var mappedBookTye = _mapper.Map<List<GetAllBookTypeCachedResponse>>(bookTypeList);
            return Result<List<GetAllBookTypeCachedResponse>>.Success(mappedBookTye);
        }
    }
}