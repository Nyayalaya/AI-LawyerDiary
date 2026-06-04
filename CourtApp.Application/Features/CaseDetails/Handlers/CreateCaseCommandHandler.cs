using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Commands;
using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Handlers
{
    public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, Result<string>>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCaseCommandHandler(ICaseRepository caseRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CaseEntity>(request.Case);

            if (await _caseRepository.IsExistsAsync(entity, Guid.Empty))
            {
                return await Result<string>
                    .FailAsync("Case already exists.");
            }

            await _caseRepository.AddAsync(entity);

            await _unitOfWork.Commit(cancellationToken);

            return await Result<string>
                .SuccessAsync("Case created successfully.");
        }
    }
}
