using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormBuilder
{
    public class DeleteTemplateQueryCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTemplateQueryCommandHandler : IRequestHandler<DeleteTemplateQueryCommand, Result<Guid>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITemplateInfoRepository _repository;
        public DeleteTemplateQueryCommandHandler(IUnitOfWork _unitOfWork, ITemplateInfoRepository _repository)
        {
            this._unitOfWork = _unitOfWork;
            this._repository = _repository;
        }
        public async Task<Result<Guid>> Handle(DeleteTemplateQueryCommand request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
