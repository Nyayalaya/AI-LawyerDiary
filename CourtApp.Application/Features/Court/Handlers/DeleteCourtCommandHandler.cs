using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Court.Handlers
{
    public class DeleteCourtCommandHandler : IRequestHandler<DeleteCourtCommand, Result<string>>
    {
        private readonly ICourtRepository _repository;
        private readonly ICourtCacheRepository _cacheRepository;

        public DeleteCourtCommandHandler(
            ICourtRepository repository,
            ICourtCacheRepository cacheRepository)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
        }

        public async Task<Result<string>> Handle(DeleteCourtCommand request, CancellationToken cancellationToken)
        {
            var court = await _repository.GetByIdAsync(request.Id);
            if (court == null)
                return Result<string>.Fail($"Court with ID '{request.Id}' not found");

            await _repository.DeleteAsync(court);
            await _cacheRepository.RemoveAsync(request.Id);

            return Result<string>.Success($"Court '{court.Name}' deleted successfully");
        }
    }
}
