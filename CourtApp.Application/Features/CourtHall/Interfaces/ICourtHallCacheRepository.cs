using CourtApp.Application.Features.CourtHall.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtHall.Interfaces
{
    public interface ICourtHallCacheRepository
    {
        Task<List<CourtHallResponse>> GetAllAsync();
        Task<List<CourtHallResponse>> GetByCourtComplexIdAsync(Guid courtComplexId);
        Task RemoveAsync();
    }
}
