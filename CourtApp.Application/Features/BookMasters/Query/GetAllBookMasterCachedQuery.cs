using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookMasters.Query
{
    public class GetAllBookMasterCachedQuery : IRequest<Result<List<GetAllBookMasterCacheResponse>>>
    {
        public Guid BookTypeId { get; set; }
        public Guid PublisherId { get; set; }
        public GetAllBookMasterCachedQuery()
        {

        }
    }

    public class GetAllBookMasterCachedQueryHandler : IRequestHandler<GetAllBookMasterCachedQuery, Result<List<GetAllBookMasterCacheResponse>>>
    {
        private readonly IBookMasterCacheRepository repository;
        private readonly IMapper _mapper;
        public GetAllBookMasterCachedQueryHandler(IBookMasterCacheRepository repository, IMapper _mapper)
        {
            this.repository = repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<GetAllBookMasterCacheResponse>>> Handle(GetAllBookMasterCachedQuery request, CancellationToken cancellationToken)
        {

            var bookTypeList = await repository.GetCachedListAsync();
            var mappedBookTye = _mapper.Map<List<GetAllBookMasterCacheResponse>>(bookTypeList);
            return Result<List<GetAllBookMasterCacheResponse>>.Success(mappedBookTye);
        }
    }
}
