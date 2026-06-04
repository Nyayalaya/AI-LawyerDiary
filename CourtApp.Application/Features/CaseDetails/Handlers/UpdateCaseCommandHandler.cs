using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Commands;
using CourtApp.Application.Features.CaseDetails.Dtos;
using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Handlers
{
    
    public class UpdateCaseCommandHandler : IRequestHandler<UpdateCaseCommand, Result<string>>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCaseCommandHandler(
            ICaseRepository caseRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(
            UpdateCaseCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _caseRepository.GetForUpdateAsync(request.Id);

            if (entity == null)
            {
                return await Result<string>
                    .FailAsync("Case not found.");
            }

            _mapper.Map(request.Case, entity);

            var exists = await _caseRepository
                .IsExistsAsync(entity, entity.Id);

            if (exists)
            {
                return await Result<string>
                    .FailAsync("Case already exists.");
            }

            await UpdateAgainstCases(entity,request.Case);

            await _caseRepository.UpdateAsync(entity);

            await _unitOfWork.Commit(cancellationToken);

            return await Result<string>
                .SuccessAsync("Case updated successfully.");
        }

        private async Task UpdateAgainstCases(CaseEntity entity, CaseRequestDto dto)
        {
            entity.CaseAgainstEntities.Clear();
            if (dto.AgainstCases?.Any() != true)
                return;

            entity.CaseAgainstEntities = dto.AgainstCases
                .Where(x => !string.IsNullOrWhiteSpace(x.CaseNo))
                .Select(x => new CaseAgainstEntity
                {
                    CourtLevelId = x.CourtLevelId,
                    CourtTypeId = x.CourtTypeId,
                    CourtDistrictId = x.CourtDistrictId,
                    CourtComplexId = x.CourtComplexId,
                    CourtId = x.CourtId,
                    CourtHallId = x.CourtHallId,

                    CaseCategoryId = x.CaseCategoryId,
                    CaseTypeId = x.CaseTypeId,

                    StateId = x.StateId,

                    CaseNo = x.CaseNo,
                    CaseYear = x.CaseYear,

                    CisNumber = x.CisNumber,
                    CisYear = x.CisYear,

                    CnrNumber = x.CnrNumber,

                    OfficerName = x.OfficerName,

                    CadreId = x.CadreId,

                    ImpugedOrderDate = x.ImpugedOrderDate
                })
                .ToList();
        }
    }
}
