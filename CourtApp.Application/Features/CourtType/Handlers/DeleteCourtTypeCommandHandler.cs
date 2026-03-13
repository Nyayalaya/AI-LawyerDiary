
using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtType.Command;
using CourtApp.Application.Features.CourtType.Services;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Handlers
{
    public class DeleteCourtTypeCommandHandler : IRequestHandler<DeleteCourtTypeCommand, Result<string>>
    {
        private readonly ICourtTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCourtTypeCommandHandler> _logger;

        public DeleteCourtTypeCommandHandler(
            ICourtTypeRepository repository,
            IUnitOfWork unitOfWork,
            ILogger<DeleteCourtTypeCommandHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(DeleteCourtTypeCommand request, CancellationToken cancellationToken)
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

                await _repository.DeleteAsync(entity);
                await _unitOfWork.Commit(cancellationToken);

                _logger.LogInformation($"Court type deleted successfully: {request.Id}");
                return Result<string>.Success("Court type deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteCourtTypeCommandHandler: {ex.Message}");
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
