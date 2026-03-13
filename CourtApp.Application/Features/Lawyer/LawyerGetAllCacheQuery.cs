using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Constants;
using CourtApp.Application.DTOs.FSTitle;
using CourtApp.Application.DTOs.Lawyer;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace CourtApp.Application.Features.Lawyer
{
    public class LawyerGetAllCacheQuery : IRequest<Result<List<LawyerResponse>>>
    {
    }
    public class LawyerGetAllCacheQueryHandler : IRequestHandler<LawyerGetAllCacheQuery, Result<List<LawyerResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly ILawyerCacheRepository _cachedRepo;
        public LawyerGetAllCacheQueryHandler(ILawyerCacheRepository _cachedRepo, IMapper _mapper)
        {
            this._cachedRepo = _cachedRepo;
            this._mapper = _mapper;
        }
        public async Task<Result<List<LawyerResponse>>> Handle(LawyerGetAllCacheQuery request, CancellationToken cancellationToken)
        {
            var dt =await  _cachedRepo.GetCachedListAsync();
            var mappedDt=_mapper.Map<List<LawyerResponse>>(dt);   
            return Result<List<LawyerResponse>>.Success(mappedDt);
        }
    }
}
