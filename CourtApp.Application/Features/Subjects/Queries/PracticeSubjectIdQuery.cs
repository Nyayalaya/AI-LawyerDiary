

using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Subjects.Queries
{
    public class PracticeSubjectIdQuery:IRequest<Result<PracticeSubjectQueryResponse>>
    {
        public Guid Id { get; set; }
        public PracticeSubjectIdQuery()
        {

        }
    }

    public class PracticeSubjectIdQueryHandler : IRequestHandler<PracticeSubjectIdQuery, Result<PracticeSubjectQueryResponse>>
    {
        private readonly ISubjectCacheRepository _repository;
        private readonly IMapper mapper;
        public PracticeSubjectIdQueryHandler(ISubjectCacheRepository _repository, IMapper mapper)
        {
            this._repository = _repository;
            this.mapper = mapper;
        }
        public async Task<Result<PracticeSubjectQueryResponse>> Handle(PracticeSubjectIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id);
            var mappeddata = mapper.Map<PracticeSubjectQueryResponse>(data);
            return Result<PracticeSubjectQueryResponse>.Success(mappeddata);
        }
    }
}
