using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseStage.Services;
using CourtApp.Application.Features.CaseStages.Command;
using CourtApp.Application.Features.CourtType.Handlers;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.Common;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Handlers
{
    public class CaseStageCreateCommandHandler : IRequestHandler<CreateCaseStageCommand, Result<string>>
    {
        private readonly ICaseStageRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        private readonly IMultiLangWordRepository _multiRepo;
        ILogger<CaseStageCreateCommandHandler> logger;
        public CaseStageCreateCommandHandler(
            ICaseStageRepository repository, 
            IMapper mapper,
            IUnitOfWork _unitOfWork, 
            IMultiLangWordRepository _multiRepo,
            ILogger<CaseStageCreateCommandHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
            this._multiRepo = _multiRepo;
            this.logger = logger;
        }

        public async Task<Result<string>> Handle(CreateCaseStageCommand request, CancellationToken cancellationToken)
        {
            // Check if a record with the same name already exists (case-insensitive)
            var existingData = repository.Entities
                .Where(e => e.Name.ToLower().Trim().Contains(request.Name.ToLower().Trim()))
                .FirstOrDefault();

            if (existingData != null)
                return await Result<string>.FailAsync("Record already exists.");

            var newStage = mapper.Map<CaseStageEntity>(request);

            await repository.InsertAsync(newStage);

            await _unitOfWork.Commit(cancellationToken);

            var keywords = request.Name.ToLower()
               .Split(' ', StringSplitOptions.RemoveEmptyEntries)
               .Select(s => new MultiLangDictEntity
               {
                   KeyWord = s
               })
               .ToList();

            if (keywords.Count > 0)
                await _unitOfWork.Commit(cancellationToken);
            logger.LogInformation($"Case stage created successfully: {newStage.Id}");
            return Result<string>.Success("Case stage created successfully.");
        }
    }
    
}
