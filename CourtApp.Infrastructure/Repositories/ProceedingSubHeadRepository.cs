using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class ProceedingSubHeadRepository : IProceedingSubHeadRepository
    {
        private readonly IRepositoryAsync<ProceedingEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public ProceedingSubHeadRepository(IRepositoryAsync<ProceedingEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<ProceedingEntity> Entities => _repository.Entities;
        public async Task<List<ProceedingEntity>> GetListAsync()
        {
            try { var data = _repository.Entities.Include(o => o.ProceedingType).ToListAsync(); return await data; }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
        public async Task<ProceedingEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }
        public async Task<Guid> InsertAsync(ProceedingEntity proceedingSubHeadEntity)
        {
            await _repository.AddAsync(proceedingSubHeadEntity);
            return proceedingSubHeadEntity.Id;
        }

        public async Task UpdateAsync(ProceedingEntity proceedingSubHeadEntity)
        {
            await _repository.UpdateAsync(proceedingSubHeadEntity);
        }
        public async Task DeleteAsync(ProceedingEntity proceedingSubHeadEntity)
        {
            await _repository.DeleteAsync(proceedingSubHeadEntity);
        }

    }
}
