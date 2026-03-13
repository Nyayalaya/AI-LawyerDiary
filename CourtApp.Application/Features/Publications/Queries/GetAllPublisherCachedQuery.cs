using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Publications.Queries
{
    public class GetAllPublisherCachedQuery : IRequest<Result<List<GetAllPublisherCachedResponse>>>
    {
        public GetAllPublisherCachedQuery()
        {

        }
    }

    public class GetAllPublisherCachedQueryHandler : IRequestHandler<GetAllPublisherCachedQuery, Result<List<GetAllPublisherCachedResponse>>>
    {
        private readonly IPublicationCacheRepository _Repository;
        private readonly IMapper _mapper;

        public GetAllPublisherCachedQueryHandler(IPublicationCacheRepository _Repository, IMapper _mapper)
        {
            this._Repository = _Repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<GetAllPublisherCachedResponse>>> Handle(GetAllPublisherCachedQuery request, CancellationToken cancellationToken)
        {
            var objList = await _Repository.GetCachedListAsync();
            var mappeData = _mapper.Map<List<GetAllPublisherCachedResponse>>(objList);
            return Result<List<GetAllPublisherCachedResponse>>.Success(mappeData);
        }
    }
}
