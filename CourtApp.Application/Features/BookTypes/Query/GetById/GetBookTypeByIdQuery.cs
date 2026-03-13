using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.BookTypes.Query.GetById
{
    public class GetBookTypeByIdQuery : IRequest<Result<GetBookTypeByIdResponse>>
    {
        public Guid Id { get; set; }

        public class GetBookTypeByIdQueryHandler : IRequestHandler<GetBookTypeByIdQuery, Result<GetBookTypeByIdResponse>>
        {
            private readonly IBookTypeCacheRepository _bookTypeCache;
            private readonly IMapper _mapper;

            public GetBookTypeByIdQueryHandler(IBookTypeCacheRepository bookTypeCache, IMapper mapper)
            {
                _bookTypeCache = bookTypeCache;
                _mapper = mapper;
            }

            public async Task<Result<GetBookTypeByIdResponse>> Handle(GetBookTypeByIdQuery query, CancellationToken cancellationToken)
            {
                var bookType = await _bookTypeCache.GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetBookTypeByIdResponse>(bookType);
                return Result<GetBookTypeByIdResponse>.Success(mappedProduct);
            }
        }
    }
}