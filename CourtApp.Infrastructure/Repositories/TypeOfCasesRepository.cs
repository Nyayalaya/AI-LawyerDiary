using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Application.CacheKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtApp.Application.Features.CaseType.Services;

namespace CourtApp.Infrastructure.Repositories
{
    public class TypeOfCasesRepository : ICaseTypeRepository
    {
        private readonly IRepositoryAsync<TypeOfCasesEntity> _repository;
        private readonly IDistributedCache _distributedCache;


        public TypeOfCasesRepository(IRepositoryAsync<TypeOfCasesEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<TypeOfCasesEntity> QryEntities => _repository.Entities;

        public async Task DeleteAsync(TypeOfCasesEntity objEntity)
        {
            await _repository.DeleteAsync(objEntity);
            await _distributedCache.RemoveAsync(TypeOfCasesCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(TypeOfCasesCacheKeys.GetKey(objEntity.Id));
        }

        public async Task<TypeOfCasesEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Include(i => i.Nature).
                Where(ck => ck. Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<TypeOfCasesEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o => o.Id).ToListAsync();
        }

        public async Task<Guid> InsertAsync(TypeOfCasesEntity objEntity)
        {
            await _repository.AddAsync(objEntity);
            await _distributedCache.RemoveAsync(TypeOfCasesCacheKeys.ListKey);
            return objEntity.Id;
        }

        public async Task UpdateAsync(TypeOfCasesEntity objEntity)
        {
            await _repository.UpdateAsync(objEntity);
            await _distributedCache.RemoveAsync(TypeOfCasesCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(TypeOfCasesCacheKeys.GetKey(objEntity.Id));
        }
    }
}
