using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtType.Command;
using CourtApp.Application.Features.CourtType.Services;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.Common;
using CourtApp.Domain.Entities;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.CourtType.Handlers
{
    public class CreateCourtTypeCommandHandler : IRequestHandler<CreateCourtTypeCommand, Result<string>>
    {
        private readonly ICourtTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMultiLangWordRepository _multiRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCourtTypeCommandHandler> _logger;

        public CreateCourtTypeCommandHandler(
            ICourtTypeRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMultiLangWordRepository multiRepo,
            ILogger<CreateCourtTypeCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _multiRepo = multiRepo;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(CreateCourtTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Normalize once
                var abbreviation = request.Code?.Trim();
                var courtType = request.Name?.Trim();
                // Duplicate CourtType check
                var isExists = await _repository.CourtTypeEntities
                    .AsNoTracking()
                    .AnyAsync(x =>
                        (abbreviation == null || x.Code == abbreviation) &&
                        (courtType == null ),
                        cancellationToken);

                if (isExists)
                {
                    _logger.LogWarning($"Duplicate court type: {courtType}");
                    return Result<string>.Fail($"{courtType} already exists.");
                }

                // Insert CourtType
                var entity = _mapper.Map<CourtTypeEntity>(request);
                //entity.Languages = _mapper.Map<List<LangEntity>>(request.Language);

                await _repository.InsertAsync(entity);
                await _unitOfWork.Commit(cancellationToken);

                // Insert keywords
                var keywords = courtType
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => new MultiLangDictEntity
                    {
                        KeyWord = s
                    })
                    .ToList();

                if (keywords.Count > 0)
                {
                    //await _multiRepo.BulkInsertAsync(keywords);
                    await _unitOfWork.Commit(cancellationToken);
                }

                _logger.LogInformation($"Court type created successfully: {entity.Id}");
                return Result<string>.Success("Court type created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateCourtTypeCommandHandler: {ex.Message}");
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
