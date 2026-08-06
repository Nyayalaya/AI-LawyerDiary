using CourtApp.Domain.Entities;
using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Services
{
    public interface ICourtTypeRepository
    {
        IQueryable<CourtTypeEntity> CourtTypeEntities { get; }

        Task<List<CourtTypeEntity>> GetListAsync();

        Task<CourtTypeEntity> GetByIdAsync(Guid CourtTypeId);

        Task<Guid> InsertAsync(CourtTypeEntity courtTypeEntity);

        Task UpdateAsync(CourtTypeEntity courtTypeEntity);

        Task DeleteAsync(CourtTypeEntity courtTypeEntity);
    }
}
