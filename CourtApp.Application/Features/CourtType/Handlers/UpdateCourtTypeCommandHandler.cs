
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtType.Command;
using CourtApp.Application.Features.CourtType.Services;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.Common;
using CourtApp.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Handlers
{
    public class UpdateCourtTypeCommandHandler : IRequestHandler<UpdateCourtTypeCommand, Result<string>>
    {
        private readonly ICourtTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMultiLangWordRepository _multiRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCourtTypeCommandHandler> _logger;

        public UpdateCourtTypeCommandHandler(
            ICourtTypeRepository repository,
            IMapper mapper,
            IMultiLangWordRepository multiRepo,
            IUnitOfWork unitOfWork,
            ILogger<UpdateCourtTypeCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _multiRepo = multiRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(UpdateCourtTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.CourtTypeEntities
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    _logger.LogWarning($"Court type not found with ID: {request.Id}");
                    return Result<string>.Fail("Court type not found.");
                }

                var abbreviation = request.Abbreviation?.Trim();
                var courtType = request.CourtType?.Trim();

                // Check for duplicates (excluding current entity)
                var isDuplicate = await _repository.CourtTypeEntities
                    .AsNoTracking()
                    .AnyAsync(x => x.Id != request.Id &&
                        ((abbreviation != null && x.Abbreviation == abbreviation) ||
                         (courtType != null && x.CourtType == courtType)),
                        cancellationToken);

                if (isDuplicate)
                {
                    _logger.LogWarning($"Duplicate court type: {courtType} or Abbreviation: {abbreviation}");
                    return Result<string>.Fail($"{courtType} already exists.");
                }

                entity.CourtType = courtType;
                entity.Abbreviation = abbreviation;
                entity.Languages = _mapper.Map<List<LangEntity>>(request.Language);

                await _repository.UpdateAsync(entity);
                await _unitOfWork.Commit(cancellationToken);

                _logger.LogInformation($"Court type updated successfully: {request.Id}");
                return Result<string>.Success("Court type updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateCourtTypeCommandHandler: {ex.Message}");
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
