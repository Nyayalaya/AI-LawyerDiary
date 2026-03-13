using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Specialization;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.Queries.Specility
{
    public class GetSpecilityQuery : IRequest<Result<List<SpecilityDto>>>
    {
    }
    public class GetSpecilityQueryHandler : IRequestHandler<GetSpecilityQuery, Result<List<SpecilityDto>>>
    {
        private readonly ISpecilityCacheRepository _repository;
        public GetSpecilityQueryHandler(ISpecilityCacheRepository _repository)
        {
            this._repository = _repository;
        }
        public async Task<Result<List<SpecilityDto>>> Handle(GetSpecilityQuery request, CancellationToken cancellationToken)
        {
            var dt = await _repository.GetCachedListAsync();
            var rs = dt.Select(s => new SpecilityDto
            {
                Id = s.Id,
                Name_En = s.Name_En,
                Name_Hn = s.Description
            }).ToList();
            return Result<List<SpecilityDto>>.Success(rs);
        }
    }
}
