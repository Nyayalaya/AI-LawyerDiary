using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Interfaces.Repositories.Common
{
    public interface IDistrictMasterRepository
    {
        IQueryable<DistrictEntity> Entities{get;}
        Task<List<DistrictEntity>> GetDistrictListByStateAsync(int StateCode);
        DistrictEntity GetDistrictById(int Id);
    }
}