using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseWorkRepository : ICaseWorkRepository
    {
        private readonly IRepositoryAsync<CaseWorkEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseWorkRepository(IRepositoryAsync<CaseWorkEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CaseWorkEntity> Entities =>
            _repository.Entities
            .Include(w => w.Work)
            .Include(w => w.Case);

        public async Task DeleteAsync(CaseWorkEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<CaseWorkEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }

        public async Task<List<CaseWorkEntity>> GetListAsync()
        {
            var data = await _repository.Entities
                .Include(w => w.Work)
                .ToListAsync();
            return data;
        }

        public async Task<List<CaseWorkEntity>> GetWorkByCaseIdAsync(Guid CaseId)
        {
            var data = await _repository
                .Entities
                .Include(w => w.WorkType)
                .Include(w => w.Work)
                .Where(w => w.CaseId == CaseId)
                .ToListAsync();
            return data;
        }

        public async Task<Guid> InsertAsync(CaseWorkEntity entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(CaseWorkEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
