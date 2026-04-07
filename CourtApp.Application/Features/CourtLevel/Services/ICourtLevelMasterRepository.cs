using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Features.CourtLevel.Services
{
    public interface ICourtLevelMasterRepository
    {
        IQueryable<CourtLevelEntity> Entities { get; }
        Task<List<CourtLevelEntity>> GetCourtLevelListAsync();
        CourtLevelEntity GetCourtLevelById(int Id);
    }
}
