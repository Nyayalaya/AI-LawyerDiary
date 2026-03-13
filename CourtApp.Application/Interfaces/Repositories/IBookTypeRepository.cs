using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IBookTypeRepository
    {
        IQueryable<BookTypeEntity> BookTypes { get; }

        Task<List<BookTypeEntity>> GetListAsync();

        Task<BookTypeEntity> GetByIdAsync(Guid bookTypeId);

        Task<Guid> InsertAsync(BookTypeEntity bookType);

        Task UpdateAsync(BookTypeEntity bookType);

        Task DeleteAsync(BookTypeEntity bookType);
    }
}
