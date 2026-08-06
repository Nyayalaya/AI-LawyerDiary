using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Application.Features.CourtLevel.Services;
using CourtApp.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Infrastructure.Repositories
{
    public class CourtLevelMasterRepository : ICourtLevelMasterRepository
    {
        private readonly IRepositoryAsync<CourtLevelEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CourtLevelMasterRepository(IRepositoryAsync<CourtLevelEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }

        public IQueryable<CourtLevelEntity> Entities => _repository.Entities;

        //public CourtLevelEntity GetCourtLevelById(int Id)
        //{
        //    return _repository.GetByIdAsync(Id).Result;
        //}

        public async Task<List<CourtLevelEntity>> GetCourtLevelListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }
    }
}
