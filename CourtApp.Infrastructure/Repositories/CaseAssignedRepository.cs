using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseAssignedRepository : ICaseAssignedRepository
    {
        private readonly IRepositoryAsync<CaseAssignedEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseAssignedRepository(IRepositoryAsync<CaseAssignedEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CaseAssignedEntity> Entities => _repository.Entities;

        public async Task DeleteRangeAsync(List<CaseAssignedEntity> entity)
        {
            await _repository.DeleteRangeAsync(entity);
        }

        public async Task<Guid> InsertAsync(CaseAssignedEntity entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(CaseAssignedEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
