using CourtApp.Application.Common;
using CourtApp.Application.Features.Court.Commands;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Court.Handlers
{
    public class UpdateCourtCommandHandler : IRequestHandler<UpdateCourtCommand, Result<Guid>>
    {
        private readonly ICourtRepository _repository;
        private readonly ICourtCacheRepository _cacheRepository;

        public UpdateCourtCommandHandler(
            ICourtRepository repository,
            ICourtCacheRepository cacheRepository)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateCourtCommand request, CancellationToken cancellationToken)
        {
            var court = await _repository.GetByIdAsync(request.Id);
            if (court == null)
                return Result<Guid>.Fail($"Court with ID '{request.Id}' not found");

            var nameNormalized = request.Name?.Trim().ToLower() ?? string.Empty;

            var existingByName = await _repository.GetByNameAndLocationAsync(nameNormalized, request.LocationId);
            if (existingByName != null && existingByName.Id != request.Id)
                return Result<Guid>.Fail($"Court with name '{request.Name}' already exists in this location");

            court.Name = request.Name?.Trim();
           
            court.CourtTypeId = request.CourtTypeId;
            

            _repository.Update(court);
            await _cacheRepository.RemoveAsync(request.Id);

            return Result<Guid>.Success(request.Id);
        }
    }
}
