using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DOType
{
    public class GetDOTypeByIdQuery:IRequest<Result<DOTypeByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class GetDOTypeByIdQueryHandler : IRequestHandler<GetDOTypeByIdQuery, Result<DOTypeByIdResponse>>
    {
        private readonly IDOTypeCacheRepository _repository;
        private readonly IMapper _mapper;
        public GetDOTypeByIdQueryHandler(IDOTypeCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<DOTypeByIdResponse>> Handle(GetDOTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);
            var mappedProduct = _mapper.Map<DOTypeByIdResponse>(detail);
            return Result<DOTypeByIdResponse>.Success(mappedProduct);
        }
    }
}
