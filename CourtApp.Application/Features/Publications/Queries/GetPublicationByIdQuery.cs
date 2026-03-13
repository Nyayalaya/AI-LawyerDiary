using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.Publications.Queries
{
    public class GetPublicationByIdQuery:IRequest<Result<GetPublicationByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetPublicationByIdQueryHandler : IRequestHandler<GetPublicationByIdQuery, Result<GetPublicationByIdResponse>>
    {
        private readonly IPublicationCacheRepository _Repository;
        private readonly IMapper _mapper;

        public GetPublicationByIdQueryHandler(IPublicationCacheRepository _Repository, IMapper _mapper)
        {
            this._Repository = _Repository;
            this._mapper = _mapper;

        }
        public async Task<Result<GetPublicationByIdResponse>> Handle(GetPublicationByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _Repository.GetByIdAsync(request.Id);
            var mappedData = _mapper.Map<GetPublicationByIdResponse>(data);
            return Result<GetPublicationByIdResponse>.Success(mappedData);
        }
    }
}
