using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseType.Services;
using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseType.Handlers
{
    public class CaseTypeDeleteCommandHandler : IRequestHandler<CaseTypeDeleteCommand, Result<string>>
    {
        private readonly ICaseTypeRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public CaseTypeDeleteCommandHandler(ICaseTypeRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CaseTypeDeleteCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            if(detail==null) return Result<string>.Fail("Record not found!");

            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<string>.Success("Record is successfully deleted!");
        }
    }
    
}
