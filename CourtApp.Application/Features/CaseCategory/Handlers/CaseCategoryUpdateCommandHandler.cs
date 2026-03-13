using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseCategory.Commands;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Handlers
{
    public class CaseCategoryUpdateCommandHandler : IRequestHandler<CaseCategoryUpdateCommand, Result<string>>
    {
        private readonly ICaseCategoryRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public CaseCategoryUpdateCommandHandler(ICaseCategoryRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<string>> Handle(CaseCategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id);
            if (entity == null) return Result<string>.Fail("No record found");

            var duplicateRecord = await repository.CaseNatures
                .Where(e => e.Id != request.Id && e.CourtTypeId.Equals(request.CourtTypeId) && e.Name_En.ToLower() == request.Name_En.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null) return Result<string>.Fail("Duplicate record found");

            entity.Name_En = request.Name_En;
            entity.Name_Hn = request.Name_Hn;
            entity.CourtTypeId = request.CourtTypeId;

            await repository.UpdateAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<string>.SuccessAsync("Category created successfully!");
        }
    }
}
