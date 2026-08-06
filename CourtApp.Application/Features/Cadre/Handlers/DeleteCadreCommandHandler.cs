using System;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Cadre.Commands;
using CourtApp.Application.Features.Cadre.Services;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;

namespace CourtApp.Application.Features.Cadre.Handlers
{
    public class DeleteCadreCommandHandler : IRequestHandler<DeleteCadreCommand, Result<string>>
    {
        private readonly ICadreMasterRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCadreCommandHandler(ICadreMasterRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteCadreCommand request, CancellationToken cancellationToken)
        {
            var cadre = await _repository.GetByIdAsync(request.Id);
            if (cadre == null)
                return Result<string>.Fail("Cadre not found");

            await _repository.DeleteAsync(cadre);
            await _unitOfWork.Commit(cancellationToken);
            return Result<string>.Success("Cadre deleted successfully");
        }
    }
}
