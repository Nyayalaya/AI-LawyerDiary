using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Location.Handlers
{
    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, Result<string>>
    {
        private readonly ILocationRepository _repository;
        private readonly ILocationCacheRepository _cacheRepository;

        public DeleteLocationCommandHandler(
            ILocationRepository repository,
            ILocationCacheRepository cacheRepository)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
        }

        public async Task<Result<string>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetByIdAsync(request.Id);
            if (location == null)
                return Result<string>.Fail("Location not found");

            await _repository.DeleteAsync(location);

            await _cacheRepository.RemoveAsync(request.Id);

            return Result<string>.Success("Location deleted successfully");
        }
    }
}
