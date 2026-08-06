using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseStage.Services;
using CourtApp.Application.Features.CaseStages.Command;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Handlers
{   
    public class CaseStageUpdateHandler : IRequestHandler<UpdateCaseStageCommand, Result<string>>
    {
        private readonly ICaseStageRepository repository;
        private readonly IMultiLangWordRepository _multiRepo;
        private readonly ILogger<CaseStageUpdateHandler> logger;
        private IUnitOfWork _unitOfWork { get; set; }
        public CaseStageUpdateHandler(ICaseStageRepository repository, 
            IUnitOfWork _unitOfWork, ILogger<CaseStageUpdateHandler> logger)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateCaseStageCommand request, CancellationToken cancellationToken)
        {

            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return await Result<string>.FailAsync("Record does not exist.");


            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await repository.Entities
                .Where(e => e.Id != request.Id && e.Name.ToLower() == request.Name.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<string>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name = request.Name;


            await repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            logger.LogInformation($"Case stage created successfully: {existingRecord.Id}");
            return Result<string>.Success("Case stage created successfully.");
        }
    }
}
