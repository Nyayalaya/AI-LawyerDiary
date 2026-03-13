using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.Client;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Queries.GetAllCached
{
    public class GetClientInfoCacheQuery : IRequest<Result<List<GetClientInfoDto>>>
    {
        public string UserId { get; set; }
    }
    public class GetClientInfoCacheQueryHandler : IRequestHandler<GetClientInfoCacheQuery, Result<List<GetClientInfoDto>>>
    {
        private readonly IClientCacheRepository _RepoClient;
        private readonly IMapper _mapper;
        public GetClientInfoCacheQueryHandler(IClientCacheRepository _RepoClient, IMapper _mapper)
        {
            this._RepoClient = _RepoClient;
            this._mapper = _mapper;
        }
        public async Task<Result<List<GetClientInfoDto>>> Handle(GetClientInfoCacheQuery request, CancellationToken cancellationToken)
        {
            List<ClientEntity> cliendtData = new List<ClientEntity>();
            if (request.UserId != "")
            {
                cliendtData = (await _RepoClient.GetCachedListAsync())
                    .Where(c => c.CreatedBy.Equals(request.UserId)).ToList();
            }
            else
                cliendtData = await _RepoClient.GetCachedListAsync();
            var dt = _mapper.Map<List<GetClientInfoDto>>(cliendtData);
            return await Result<List<GetClientInfoDto>>.SuccessAsync(dt);
        }
    }
}
