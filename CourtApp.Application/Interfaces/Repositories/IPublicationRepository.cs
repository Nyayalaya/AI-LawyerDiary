using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
  public  interface IPublicationRepository
    {
        IQueryable<PublisherEntity> BookTypes { get; }

        Task<List<PublisherEntity>> GetListAsync();

        Task<PublisherEntity> GetByIdAsync(Guid bookTypeId);

        Task<Guid> InsertAsync(PublisherEntity bookType);

        Task UpdateAsync(PublisherEntity bookType);

        Task DeleteAsync(PublisherEntity bookType);
    }
}
