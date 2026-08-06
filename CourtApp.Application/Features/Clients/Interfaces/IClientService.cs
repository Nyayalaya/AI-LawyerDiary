using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Features.Clients.Interfaces
{
    /// <summary>
    /// Interface for Client feature operations
    /// Defines contract for client-related business logic
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Creates a new client
        /// </summary>
        /// <param name="clientEntity">The client entity to create</param>
        /// <returns>Task containing the created client ID</returns>
        Task<Guid> CreateClientAsync(ClientEntity clientEntity);

        /// <summary>
        /// Updates an existing client
        /// </summary>
        /// <param name="clientEntity">The client entity to update</param>
        /// <returns>Task representing the async operation</returns>
        Task UpdateClientAsync(ClientEntity clientEntity);

        /// <summary>
        /// Deletes a client by ID
        /// </summary>
        /// <param name="id">The client ID to delete</param>
        /// <returns>Task representing the async operation</returns>
        Task DeleteClientAsync(Guid id);

        /// <summary>
        /// Gets a client by ID
        /// </summary>
        /// <param name="id">The client ID</param>
        /// <returns>The client entity or null if not found</returns>
        Task<ClientEntity> GetClientByIdAsync(Guid id);

        /// <summary>
        /// Gets all clients
        /// </summary>
        /// <returns>List of all client entities</returns>
        Task<List<ClientEntity>> GetAllClientsAsync();

        /// <summary>
        /// Searches clients by multiple criteria
        /// </summary>
        /// <param name="searchTerm">Search term (name, email, mobile)</param>
        /// <returns>List of matching client entities</returns>
        Task<List<ClientEntity>> SearchClientsAsync(string searchTerm);
    }
}
