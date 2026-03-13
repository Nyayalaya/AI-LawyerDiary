using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        IQueryable<ClientEntity> Clients { get; }

        Task<List<ClientEntity>> GetListAsync();

        Task<ClientEntity> GetByIdAsync(Guid clientId);

        Task<Guid> InsertAsync(ClientEntity client);

        Task UpdateAsync(ClientEntity client);

        Task DeleteAsync(ClientEntity client);
    }
}
