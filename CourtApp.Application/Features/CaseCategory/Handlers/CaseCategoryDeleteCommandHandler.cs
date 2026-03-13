using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseCategory.Commands;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Handlers
{
    public class CaseCategoryDeleteCommandHandler : IRequestHandler<CaseCategoryDeleteCommand, Result<string>>
    {
        private readonly ICaseCategoryRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public CaseCategoryDeleteCommandHandler(ICaseCategoryRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CaseCategoryDeleteCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<string>.Success("Record is deleted successfully!");
        }
    }
}
