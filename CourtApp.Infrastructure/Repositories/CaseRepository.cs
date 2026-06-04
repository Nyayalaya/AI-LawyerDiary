using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly IRepositoryAsync<CaseEntity> _repository;
        public CaseRepository(IRepositoryAsync<CaseEntity> repository)
        {
            this._repository = repository;
        }

        public IQueryable<CaseEntity> Cases => _repository.Entities;

        public async Task AddAsync(CaseEntity entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(CaseEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<CaseEntity> GetByIdAsync(Guid id)
        {
            return await _repository.Entities
                       .Include(x => x.Court)
                       .Include(x => x.CaseCategory)
                       .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CaseEntity?> GetCaseHistoryAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = _repository.Entities
                .AsNoTracking()
                .Where(x => x.Id == id);

            query = query
                .Include(x => x.CaseProceedingEntities)
                    .ThenInclude(cp => cp.Head);

            query = query
                .Include(x => x.CaseProceedingEntities)
                    .ThenInclude(cp => cp.SubHead);

            query = query
                .Include(x => x.CaseProceedingEntities)
                    .ThenInclude(cp => cp.ProcWork)
                        .ThenInclude(pw => pw.Works);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<CaseEntity> GetForUpdateAsync(Guid id)
        {
            return await Cases
                .Include(x => x.CaseAgainstEntities)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task<bool> IsExistsAsync(
    CaseEntity entity,
    Guid excludeId)
        {
            return await Cases.AnyAsync(x =>
                x.Id != excludeId &&
                !x.IsDeleted &&
                x.CourtTypeId == entity.CourtTypeId &&
                x.CourtId == entity.CourtId &&
                x.CaseCategoryId == entity.CaseCategoryId &&
                x.FirstTitleId == entity.FirstTitleId &&
                x.SecondTitleId == entity.SecondTitleId &&
                x.ParentCaseId == entity.ParentCaseId);
        }

        public async Task UpdateAsync(CaseEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
