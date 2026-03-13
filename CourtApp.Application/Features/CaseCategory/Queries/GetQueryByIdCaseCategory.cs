using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Features.CaseCategory.Dto;
using CourtApp.Application.Features.CaseCategory.Services;

namespace CourtApp.Application.Features.CaseCategory.Queries
{
    public class GetQueryByIdCaseCategory : IRequest<Result<CaseCategoryByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class QryCmdByIdCaseCategoryHandler : IRequestHandler<GetQueryByIdCaseCategory, Result<CaseCategoryByIdResponse>>
    {
        private readonly ICaseCategoryCacheRepository _repository;
        private readonly IMapper mapper;
        public QryCmdByIdCaseCategoryHandler(ICaseCategoryCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this.mapper = _mapper;
        }


        public async Task<Result<CaseCategoryByIdResponse>> Handle(GetQueryByIdCaseCategory request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetCachedListAsync();
            var dt = data.Where(w => w.Id == request.Id).FirstOrDefault();
            var mappeddata = mapper.Map<CaseCategoryByIdResponse>(dt);
            return Result<CaseCategoryByIdResponse>.Success(mappeddata);
        }
    }
}
