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
    public class WorkMasterSubRepository : IWorkMasterSubRepository
    {
        private readonly IRepositoryAsync<WorkMasterSubEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public WorkMasterSubRepository(IRepositoryAsync<WorkMasterSubEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<WorkMasterSubEntity> Entities => _repository.Entities.Include(o => o.Work);
        public async Task<List<WorkMasterSubEntity>> GetListAsync()
        {
            try { var data = _repository.Entities.Include(o => o.Work).ToListAsync(); return await data; }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
        public async Task<WorkMasterSubEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities.Include(w => w.Work)
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }
        public async Task<Guid> InsertAsync(WorkMasterSubEntity workMasterEntity)
        {
            await _repository.AddAsync(workMasterEntity);
            return workMasterEntity.Id;
        }

        public async Task UpdateAsync(WorkMasterSubEntity workMasterEntity)
        {
            await _repository.UpdateAsync(workMasterEntity);
        }
        public async Task DeleteAsync(WorkMasterSubEntity workMasterEntity)
        {
            await _repository.DeleteAsync(workMasterEntity);
        }

    }
}
