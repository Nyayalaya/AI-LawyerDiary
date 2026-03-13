using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using CourtApp.Application.Features.CaseType.Services;

namespace CourtApp.Application.Features.Typeofcasess.Query
{
    public class TypeOfCasesByIdQuery : IRequest<Result<TypeOfCasesQueryByIdResponse>>
    {
        public Guid Id { get; set; }
        public TypeOfCasesByIdQuery()
        {

        }
    }

    public class CaseKindByIdQueryCommandHandler : IRequestHandler<TypeOfCasesByIdQuery, Result<TypeOfCasesQueryByIdResponse>>
    {
        private readonly ICaseTypeRepository _repository;
        private readonly IMapper mapper;
        public CaseKindByIdQueryCommandHandler(ICaseTypeRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this.mapper = _mapper;
        }
        public async Task<Result<TypeOfCasesQueryByIdResponse>> Handle(TypeOfCasesByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id);
            var mappeddata = mapper.Map<TypeOfCasesQueryByIdResponse>(data);
            return Result<TypeOfCasesQueryByIdResponse>.Success(mappeddata);
        }
    }
}
