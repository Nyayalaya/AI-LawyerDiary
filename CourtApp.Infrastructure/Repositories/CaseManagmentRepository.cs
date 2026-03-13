using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseManagmentRepository : ICaseManagmentRepository
    {
        private readonly IRepositoryAsync<CaseDetailEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseManagmentRepository(IRepositoryAsync<CaseDetailEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CaseDetailEntity> Entities => _repository.Entities;
        public async Task<List<CaseDetailEntity>> GetListAsync()
        {
            try { var data = _repository.Entities.ToListAsync(); return await data; }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
        public async Task<CaseDetailEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }
        //public async Task<Guid> InsertAsync(CaseDetailEntity workMasterEntity)
        //{
        //foreach (var item in workMasterEntity.AgainstCaseDetails)
        //{
        //    item. = workMasterEntity.Id;
        //}
        //await _repository.AddAsync(workMasterEntity);
        //return workMasterEntity.Id;
        //}

        public async Task UpdateAsync(CaseDetailEntity workMasterEntity)
        {
            await _repository.UpdateAsync(workMasterEntity);
        }
        public async Task DeleteAsync(CaseDetailEntity workMasterEntity)
        {
            await _repository.DeleteAsync(workMasterEntity);
        }

    }
}
