using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
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
        private readonly IRepositoryAsync<WorkMasterEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public WorkMasterRepository(IRepositoryAsync<WorkMasterEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<WorkMasterEntity> Entities => _repository.Entities;
        public async Task<List<WorkMasterEntity>> GetListAsync()
        {
            try { var data = _repository.Entities.ToListAsync(); return await data; }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
        public async Task<WorkMasterEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }
        public async Task<Guid> InsertAsync(WorkMasterEntity workMasterEntity)
        {
            await _repository.AddAsync(workMasterEntity);
            return workMasterEntity.Id;
        }

        public async Task UpdateAsync(WorkMasterEntity workMasterEntity)
        {
            await _repository.UpdateAsync(workMasterEntity);
        }
        public async Task DeleteAsync(WorkMasterEntity workMasterEntity)
        {
            await _repository.DeleteAsync(workMasterEntity);
        }

    }
}
