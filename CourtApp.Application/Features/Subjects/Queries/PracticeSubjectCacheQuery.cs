using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Subjects.Queries
{
    public class PracticeSubjectCacheQuery : IRequest<Result<List<PracticeSubjectCacheQueryResponse>>>
    {
        public PracticeSubjectCacheQuery()
        {

        }
    }

    public class PracticeSubjectCacheQueryHandler : IRequestHandler<PracticeSubjectCacheQuery, Result<List<PracticeSubjectCacheQueryResponse>>>
    {
        private readonly ISubjectCacheRepository _repository;
        private readonly IMapper mapper;

        public PracticeSubjectCacheQueryHandler(ISubjectCacheRepository _bookTypeCache, IMapper mapper)
        {
            this._repository = _bookTypeCache;
            this.mapper = mapper;
        }

        public async Task<Result<List<PracticeSubjectCacheQueryResponse>>> Handle(PracticeSubjectCacheQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetCachedListAsync();
            var mappeddata = mapper.Map<List<PracticeSubjectCacheQueryResponse>>(data);
            return Result<List<PracticeSubjectCacheQueryResponse>>.Success(mappeddata);
        }
    }
}
