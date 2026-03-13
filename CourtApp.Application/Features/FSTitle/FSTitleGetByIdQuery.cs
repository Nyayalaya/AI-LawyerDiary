using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.FSTitle;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using CourtApp.Application.Interfaces.Repositories;

namespace CourtApp.Application.Features.FSTitle
{
    public class FSTitleGetByIdQuery : IRequest<Result<FSTitleByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class FSTitleGetByIdQueryHandler : IRequestHandler<FSTitleGetByIdQuery, Result<FSTitleByIdResponse>>
    {
        private readonly IFSTitleRepository _repository;
        private readonly IMapper _mapper;
        public FSTitleGetByIdQueryHandler(IFSTitleRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<FSTitleByIdResponse>> Handle(FSTitleGetByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);
            var mappedProduct = _mapper.Map<FSTitleByIdResponse>(detail);
            return Result<FSTitleByIdResponse>.Success(mappedProduct);
        }
    }
}
