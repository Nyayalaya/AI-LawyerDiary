using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Queries.GetAllCached
{
    public class GetUserClientLookUpQuery:IRequest<Result<List<DdlGuidStringDto>>>
    {
        public string UserId { get; set; }
    }

    public class GetUserClientLookUpQueryHandler : IRequestHandler<GetUserClientLookUpQuery, Result<List<DdlGuidStringDto>>>
    {
        private readonly IClientCacheRepository _RepoClient;
        public GetUserClientLookUpQueryHandler(IClientCacheRepository RepoClient)
        {
            _RepoClient = RepoClient;
        }
        public async Task<Result<List<DdlGuidStringDto>>> Handle(GetUserClientLookUpQuery request, CancellationToken cancellationToken)
        {
            List<DdlGuidStringDto> cliendtData = new List<DdlGuidStringDto>();
            if (request.UserId != "")
            {
                cliendtData = (await _RepoClient.GetCachedListAsync())
                    .Where(c => c.CreatedBy.Equals(request.UserId))
                    .Select(c => new DdlGuidStringDto { Id = c.Id, Name = c.Name }).ToList();
            }
            else
                cliendtData = (await _RepoClient.GetCachedListAsync())
                    .Select(c => new DdlGuidStringDto { Id = c.Id, Name = c.Name }).ToList();
            return await Result<List<DdlGuidStringDto>>.SuccessAsync(cliendtData);
        }
    }
}
