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
    public class WorkMasterRepository : IWorkMasterRepository
    {
        private readonly IRepositoryAsync<WorkTypeEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public WorkMasterRepository(IRepositoryAsync<WorkTypeEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<WorkTypeEntity> Entities => _repository.Entities;
        public async Task<List<WorkTypeEntity>> GetListAsync()
        {
            try { var data = _repository.Entities.ToListAsync(); return await data; }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
        public async Task<WorkTypeEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }
        public async Task<Guid> InsertAsync(WorkTypeEntity workMasterEntity)
        {
            await _repository.AddAsync(workMasterEntity);
            return workMasterEntity.Id;
        }

        public async Task UpdateAsync(WorkTypeEntity workMasterEntity)
        {
            await _repository.UpdateAsync(workMasterEntity);
        }
        public async Task DeleteAsync(WorkTypeEntity workMasterEntity)
        {
            await _repository.DeleteAsync(workMasterEntity);
        }

    }
}
