using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Domain.Entities;
using CourtApp.Entities.Common;

namespace CourtApp.Application.Features.State.Services
{
    public interface IStateMasterRepository
    {
        IQueryable<StateEntity> Entities { get; }
        Task<List<StateEntity>> GetStateListAsync();
        StateEntity GetStateById(int Id);
    }
}