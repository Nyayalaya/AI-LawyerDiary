using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookMasters.Query
{
    public class GetBookMasterByIdQuery : IRequest<Result<GetBookMasterByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class GetBookMasterByIdQueryHandler : IRequestHandler<GetBookMasterByIdQuery, Result<GetBookMasterByIdResponse>>
    {
        private readonly IBookMasterCacheRepository _bookMasterCache;
        private readonly IMapper _mapper;
        public GetBookMasterByIdQueryHandler(IBookMasterCacheRepository _bookMasterCache, IMapper _mapper)
        {
            this._bookMasterCache = _bookMasterCache;
            this._mapper = _mapper;
        }
        public async Task<Result<GetBookMasterByIdResponse>> Handle(GetBookMasterByIdQuery request, CancellationToken cancellationToken)
        {
            var bookdetail = await _bookMasterCache.GetByIdAsync(request.Id);
            var mappedBookDetail = _mapper.Map<GetBookMasterByIdResponse>(bookdetail);
            return Result<GetBookMasterByIdResponse>.Success(mappedBookDetail);
        }
    }
}
