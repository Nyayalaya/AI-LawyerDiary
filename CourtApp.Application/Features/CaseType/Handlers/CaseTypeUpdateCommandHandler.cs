using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Application.Features.CaseType.Services;
using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseType.Handlers
{
    public class CaseTypeUpdateCommandHandler : IRequestHandler<CaseTypeUpdateCommand, Result<string>>
    {
        private readonly ICaseTypeRepository repository;
        private readonly ICaseCategoryRepository caseNatureRepository;
        private IUnitOfWork _unitOfWork { get; set; }
        public CaseTypeUpdateCommandHandler(ICaseTypeRepository repository,
            IUnitOfWork _unitOfWork,
            ICaseCategoryRepository caseNatureRepository)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
            this.caseNatureRepository = caseNatureRepository;
        }

        public async Task<Result<string>> Handle(CaseTypeUpdateCommand request, CancellationToken cancellationToken)
        {

            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null) return Result<string>.Fail("Record does not exist.");

            var duplicateRecord = await repository.QryEntities
                .Where(e => e.Id != request.Id
                                && e.CourtTypeId == request.CourtTypeId
                                && e.NatureId == request.NatureId
                                && e.Name_En.ToLower() == request.Name_En.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null) return Result<string>.Fail("Another record with the same name already exists.");


            existingRecord.Name_En = request.Name_En;
            existingRecord.Name_Hn = request.Name_Hn;

            await repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<string>.Success("Record inserted successfully!");

        }
    }
}
    
