using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Queries.GetById
{
    public class GetClientByIdQuery : IRequest<Result<GetClientByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Result<GetClientByIdResponse>>
    {
        private readonly IClientRepository _Repository;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IClientRepository _Repository, IMapper mapper)
        {
            this._Repository = _Repository;
            _mapper = mapper;
        }

        public async Task<Result<GetClientByIdResponse>> Handle(GetClientByIdQuery query, CancellationToken cancellationToken)
        {
            var client = await _Repository.GetByIdAsync(query.Id);
            var mappedClient = _mapper.Map<GetClientByIdResponse>(client);
            return Result<GetClientByIdResponse>.Success(mappedClient);
        }
    }
}